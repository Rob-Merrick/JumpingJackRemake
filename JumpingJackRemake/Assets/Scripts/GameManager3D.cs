using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager3D : Manager<GameManager3D>
{
	public bool IsReady { get; private set; } = false;

	private void Start()
	{
		ScreenManager3D.Instance.FadeToColor(Color.black, timeToFade: 0.0F, callback: LoseLifeFadedToBlack);
	}

	public void LoseLife()
	{
		IsReady = false;
		ScreenManager3D.Instance.FadeToColor(Color.black, callback: LoseLifeFadedToBlack);
	}

	public void WinLevel()
	{
		IsReady = false;
		this.DoAfter(seconds: 5.0F, () => ScreenManager3D.Instance.FadeToColor(Color.white, callback: WinLevelFadedToWhite));
	}

	private void Restart()
	{
		SpawnManager3D.Instance.Restart();
		FloorManager3D.Instance.Restart();
		HazardManager3D.Instance.Restart();
		LennyManager3D.Instance.Restart();
	}

	private void LoseLifeFadedToBlack()
	{
		Restart();
		this.DoAfter(seconds: 3.0F, () => ScreenManager3D.Instance.FadeFromColor(Color.black, callback: PrepareForCountdown));
	}

	private void PrepareForCountdown()
	{
		this.DoAfter(seconds: 1.0F, () => ScreenManager3D.Instance.StartCountdown(() => IsReady = true));
	}

	private void WinLevelFadedToWhite()
	{
		Restart();
		this.DoAfter(seconds: 3.0F, () => ScreenManager3D.Instance.FadeFromColor(Color.white, callback: PrepareForCountdown));
	}
}
