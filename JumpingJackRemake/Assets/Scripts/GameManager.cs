using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Manager<GameManager>
{
	private int _currentLevel = 0;

	public void WinLevel()
	{
		_currentLevel++;
		Time.timeScale = 0.0F;
	}

	public void Restart()
	{
		_currentLevel = 0;
		Time.timeScale = 0.0F;
		ScoreManager.Instance.Restart();
	}

	private void DisplayScreen(int screenIndex)
	{

	}
}
