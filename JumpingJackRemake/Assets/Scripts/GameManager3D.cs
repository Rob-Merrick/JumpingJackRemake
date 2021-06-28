using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager3D : Manager<GameManager3D>
{
	[SerializeField] private GameObject _pauseWindow;

	public bool IsReady { get; private set; } = false;
	public int Level { get; private set; } = 1;
	public static int HighLevel { get; private set; } = 0;

	private void Start()
	{
		ScreenManager3D.Instance.FadeToColor(Color.black, timeToFade: 0.0F, callback: LevelFirstRun);
	}

	private void Update()
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
				Time.timeScale = 1.0F;
				SceneManager.LoadScene("Menu");
			}
		}
	}

	public void LoseLife()
	{
		IsReady = false;
		ScreenManager3D.Instance.FadeToColor(Color.black, callback: LoseLifeFadedToBlack);
	}

	public void LoseGame()
	{
		IsReady = false;
		ScreenManager3D.Instance.FadeToColor(Color.black, timeToFade: 5.0F, callback: LoseGameFadedToBlack);
	}

	public void WinLevel()
	{
		IsReady = false;
		HazardManager3D.Instance.SetAnimationPlayingState(isPlaying: false);
		this.DoAfter(seconds: 5.0F, () => ScreenManager3D.Instance.FadeToColor(Color.white, callback: WinLevelFadedToWhite));
	}

	private void Restart()
	{
		SpawnManager3D.Instance.Restart();
		FloorManager3D.Instance.Restart();
		HazardManager3D.Instance.Restart();
		LennyManager3D.Instance.Restart();
	}

	private void LevelFirstRun()
	{
		Restart();
		this.DoAfter(seconds: 3.0F, () => ScreenManager3D.Instance.FadeFromColor(Color.black, callback: PrepareForCountdown)); //TODO: Add in the cutscene above this line
	}

	private void LoseLifeFadedToBlack()
	{
		Restart();
		LennyManager3D.Instance.DecreaseLife();
		ScreenManager3D.Instance.ShowLostLife();
		this.DoAfter(seconds: 3.0F, () => ScreenManager3D.Instance.FadeFromColor(Color.black, callback: PrepareForCountdown));
	}

	private void LoseGameFadedToBlack()
	{
		int lastLevel = Level;
		Level = 1;
		HighLevel = Mathf.Max(Level, HighLevel);
		Restart();
		LennyManager3D.Instance.ResetLives();
		this.DoAfter(seconds: 5.0F, () => ScreenManager3D.Instance.DisplayGameOver(lastLevel, HighLevel, () => ScreenManager3D.Instance.FadeFromColor(Color.black, callback: PrepareForCountdown)));
	}

	private void WinLevelFadedToWhite()
	{
		Level++;
		HighLevel = Mathf.Max(Level, HighLevel);
		Restart();
		this.DoAfter(seconds: 1.0F, () => ScreenManager3D.Instance.ShowGainedLevel());
		this.DoAfter(seconds: 6.0F, () => ScreenManager3D.Instance.FadeFromColor(Color.white, callback: PrepareForCountdown));
	}

	private void PrepareForCountdown()
	{
		this.DoAfter(seconds: 1.0F, () => ScreenManager3D.Instance.StartCountdown(() => IsReady = true));
	}
}
