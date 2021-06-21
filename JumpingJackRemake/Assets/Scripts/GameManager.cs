using System;
using UnityEngine;

public class GameManager : Manager<GameManager>
{
	[SerializeField] private GameObject _screensContainer;

	private bool _isMainScreenVisible = false;
	private ScreenInfo[] _screens;

	public bool IsReady { get; private set; } = false;
	public int CurrentLevel { get; private set; } = 0;

	private void Start()
	{
		_screensContainer.SetActive(true);
		_screens = _screensContainer.GetComponentsInChildren<ScreenInfo>();
		Initialize();
		HideAllScreens();
		DisplayMainScreen();
	}

	private void Update()
	{
		if(_isMainScreenVisible && Input.GetKeyDown(KeyCode.Return))
		{
			Ready();
		}
	}

	public void Ready()
	{
		HideAllScreens();
		IsReady = true;
		_isMainScreenVisible = false;
	}

	public void WinLevel()
	{
		CurrentLevel++;
		Initialize();
		DisplayLevelScreen();
	}

	public void Restart()
	{
		CurrentLevel = 0;
		ScoreManager.Instance.Restart();
		Initialize();
		//DisplayLevelScreen();
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
	}

	private void DisplayLevelScreen()
	{
		DisplayScreen((screen) => screen.ScreenType == ScreenInfoType.Level && screen.LevelNumber == CurrentLevel);
	}

	private void DisplayScreen(Func<ScreenInfo, bool> screenCondition)
	{
		foreach(ScreenInfo screen in _screens)
		{
			screen.gameObject.SetActive(screenCondition.Invoke(screen));
		}
	}

	private void Initialize()
	{
		IsReady = false;
		LennyManager.Instance.Restart();
		LennyManager.Instance.Animator.SetOnlyTrigger("Idle");
		HoleManager.Instance.Restart();
		HazardManager.Instance.Restart();
	}
}
