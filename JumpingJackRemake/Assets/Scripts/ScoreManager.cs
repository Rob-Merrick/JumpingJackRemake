public class ScoreManager : Manager<ScoreManager>
{
	private int _highScore = 0;
	private int _currentScore = 0;

	public int HighScore => _highScore;
	public int CurrentScore => _currentScore;
	public bool IsNewHighScore { get; private set; } = false;

	public void AddPoints(int points)
	{
		if(points <= 0)
		{
			throw new System.Exception("Score points must be positive");
		}

		_currentScore += points;

		if(_currentScore > _highScore)
		{
			IsNewHighScore = true;
			_highScore = _currentScore;
		}
	}

	public void Restart()
	{
		_currentScore = 0;
		IsNewHighScore = false;
	}
}
