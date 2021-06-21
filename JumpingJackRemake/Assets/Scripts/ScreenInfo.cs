using UnityEngine;

public enum ScreenInfoType
{
	Main = 1,
	Final = 2,
	GameOver = 3,
	Level = 4
}

public class ScreenInfo : MonoBehaviour
{
	[SerializeField] private ScreenInfoType _screenType = ScreenInfoType.Level;
	[SerializeField] private int _levelNumber;

	public ScreenInfoType ScreenType => _screenType;
	public bool IsExtraLiveVisible => _levelNumber > 1 && _levelNumber % 5 == 1;
	public int LevelNumber => _levelNumber;
}
