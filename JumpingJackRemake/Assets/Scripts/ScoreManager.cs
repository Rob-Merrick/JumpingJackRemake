using UnityEngine;

public class ScoreManager : Manager<ScoreManager>
{
	private int _highScore = 0;
	private int _currentScore = 0;

	public int HighScore => _highScore;
	public int CurrentCore => _currentScore;

	public void AddPoints(int points)
	{
		if(points <= 0)
		{
			throw new System.Exception("Score points must be positive");
		}

		_currentScore += points;
		_highScore = Mathf.Max(_highScore, _currentScore);
	}

	public void Restart()
	{
		_currentScore = 0;
	}
}
