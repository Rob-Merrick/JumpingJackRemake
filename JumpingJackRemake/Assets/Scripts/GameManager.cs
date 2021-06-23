using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Manager<GameManager>
{
	[SerializeField] private GameObject _screensContainer;
	[SerializeField] private GameObject _konamiCodeWindow;
	[SerializeField] private GameObject _pauseWindow;
	[SerializeField] private GameOverScreen _gameOverScreen;

	private bool _isMainScreenVisible = false;
	private bool _isGameOverScreenVisible = false;
	private bool _isMainMenuLoaded = false;
	private int _konamiCodeIndex = 0;
	private float _konamiCodeTimer = 0.0F;
	private ScreenInfo[] _screens;
	private readonly KeyCode[] _konamiCode = { KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.RightArrow };
	private readonly IDictionary<TextScroller, bool> _textScrollerInitiallyEnabledLookup = new Dictionary<TextScroller, bool>();
	private readonly IDictionary<ExtraLifeUI, bool> _extraLifeInitiallyEnabledLookup = new Dictionary<ExtraLifeUI, bool>();

	public bool IsReady { get; private set; } = false;
	public int CurrentLevel { get; private set; } = 0;

	private void Start()
	{
		_screensContainer.SetActive(true);
		_screens = _screensContainer.GetComponentsInChildren<ScreenInfo>();
		RecordInitialEnabledStates();
		Initialize();
		HideAllScreens();
		DisplayMainScreen();
	}

	private void Update()
	{
		if(!_isMainMenuLoaded)
		{
			return;
		}

		CheckForPauseMenu();

		if(!_pauseWindow.activeSelf && !CheckForKonamiCode() && (_isGameOverScreenVisible || _isMainScreenVisible) && Input.GetKeyDown(KeyCode.Return))
		{
			if(_isGameOverScreenVisible)
			{
				Restart();
			}

			Ready();
		}
	}

	public void MainMenuLoaded()
	{
		_isMainMenuLoaded = true;
	}

	public void Ready()
	{
		HideAllScreens();
		IsReady = true;
		_isMainScreenVisible = false;
		_isGameOverScreenVisible = false;
	}

	public void WinLevel()
	{
		CurrentLevel++;
		Initialize();

		if(CurrentLevel > 20)
		{
			CurrentLevel = 20;
			WinGame();
		}
		else
		{
			DisplayLevelScreen();

			if(CurrentLevel > 1 && CurrentLevel % 5 == 1)
			{
				LennyManager.Instance.GainLife();
			}
		}
	}

	private void Restart()
	{
		CurrentLevel = 0;
		ScoreManager.Instance.Restart();
		LennyManager.Instance.ResetLives();
		Initialize();
	}

	public void GameOver()
	{
		DisplayGameOverScreen();
	}

	private void WinGame()
	{
		DisplayFinalScreen();
	}

	private void HideAllScreens()
	{
		DisplayScreen((screen) => false);
	}

	private void DisplayMainScreen()
	{
		DisplayScreen((screen) => screen.ScreenType == ScreenInfoType.Main);
		_isMainScreenVisible = true;
	}

	private void DisplayFinalScreen()
	{
		DisplayScreen((screen) => screen.ScreenType == ScreenInfoType.Final);
	}

	private void DisplayGameOverScreen()
	{
		DisplayScreen((screen) => screen.ScreenType == ScreenInfoType.GameOver);
		_gameOverScreen.RefreshText();
		_isGameOverScreenVisible = true;
	}

	private void DisplayLevelScreen()
	{
		DisplayScreen((screen) => screen.ScreenType == ScreenInfoType.Level && screen.LevelNumber == CurrentLevel);
	}

	private void DisplayScreen(Func<ScreenInfo, bool> screenCondition)
	{
		foreach(ScreenInfo screen in _screens)
		{
			bool isActive = screenCondition.Invoke(screen);
			screen.gameObject.SetActive(isActive);
			TextScroller[] textScrollers = screen.GetComponentsInChildren<TextScroller>(includeInactive: true);
			ExtraLifeUI[] extraLifeUIs = screen.GetComponentsInChildren<ExtraLifeUI>(includeInactive: true);

			for(int i = 0; i < textScrollers.Length; i++)
			{
				TextScroller textScroller = textScrollers[i];
				textScrollers[i].enabled = _textScrollerInitiallyEnabledLookup[textScroller];
			}

			for(int i = 0; i < extraLifeUIs.Length; i++)
			{
				ExtraLifeUI extraLifeUI = extraLifeUIs[i];
				extraLifeUIs[i].gameObject.SetActive(_extraLifeInitiallyEnabledLookup[extraLifeUI]);
			}
		}
	}

	private void Initialize()
	{
		IsReady = false;
		PlacementManager.Instance.Restart();
		LennyManager.Instance.Restart();
		LennyManager.Instance.Animator.SetOnlyTrigger("Idle");
		HoleManager.Instance.Restart();
		HazardManager.Instance.Restart();
	}

	private void RecordInitialEnabledStates()
	{
		TextScroller[] textScrollers = Resources.FindObjectsOfTypeAll<TextScroller>();
		ExtraLifeUI[] extraLifeUIs = Resources.FindObjectsOfTypeAll<ExtraLifeUI>();

		for(int i = 0; i < textScrollers.Length; i++)
		{
			TextScroller textScroller = textScrollers[i];
			_textScrollerInitiallyEnabledLookup.Add(textScroller, textScroller.enabled);
		}

		for(int i = 0; i < extraLifeUIs.Length; i++)
		{
			ExtraLifeUI extraLifeUI = extraLifeUIs[i];
			_extraLifeInitiallyEnabledLookup.Add(extraLifeUI, extraLifeUI.gameObject.activeSelf);
		}
	}

	private bool CheckForKonamiCode()
	{
		if(LennyManager.Instance.IsKonamiCodeEnabled)
		{
			return false;
		}

		if(_konamiCodeWindow.activeSelf)
		{
			if(Input.GetKeyDown(KeyCode.Return))
			{
				LennyManager.Instance.IsKonamiCodeEnabled = true;
				_konamiCodeWindow.SetActive(false);
				Time.timeScale = 1.0F;
				return true;
			}
		}
		else if(Input.GetKeyDown(_konamiCode[_konamiCodeIndex]))
		{
			_konamiCodeIndex++;
			_konamiCodeTimer = 0.0F;

			if(_konamiCodeIndex >= _konamiCode.Length)
			{
				Time.timeScale = 0.0F;
				_konamiCodeWindow.SetActive(true);
			}
		}
		else if(_konamiCodeTimer >= 0.75F)
		{
			_konamiCodeTimer = 0.0F;
			_konamiCodeIndex = 0;
		}
		else
		{
			_konamiCodeTimer += Time.deltaTime;
		}

		return false;
	}

	private void CheckForPauseMenu()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			_pauseWindow.SetActive(!_pauseWindow.activeSelf);
			Time.timeScale = 1.0F - Time.timeScale;
		}

		if(_pauseWindow.activeSelf)
		{
			if(Input.GetKeyDown(KeyCode.M))
			{
				SceneManager.LoadScene("Menu");
			}
		}
	}
}
