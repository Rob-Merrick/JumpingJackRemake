using UnityEngine;

public class GameSettingsManager : Manager<GameSettingsManager>
{
	[SerializeField] [Range(10.0F, 100.0F)] private float _runSpeed = 85.0F;

	public float RunSpeed => _runSpeed;
}
