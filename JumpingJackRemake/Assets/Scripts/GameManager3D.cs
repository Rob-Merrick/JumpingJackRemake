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
		this.DoAfter(seconds: 3.0F, () => ScreenManager3D.Instance.FadeFromColor(Color.black, callback: LoseLifeFadedFromBlack));
	}

	private void LoseLifeFadedFromBlack()
	{
		this.DoAfter(seconds: 1.0F, () => ScreenManager3D.Instance.StartCountdown(() => IsReady = true));
	}
}
