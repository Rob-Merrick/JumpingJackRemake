using UnityEngine;

public class GameSettingsManager : MonoBehaviour
{
	[SerializeField] [Range(10.0F, 100.0F)] private float _runSpeed = 85.0F;

	public static GameSettingsManager Instance { get; private set; }

	public float RunSpeed => _runSpeed;

	private void Awake()
	{
		if(Instance != null)
		{
			throw new System.Exception($"Attemption to overwrite the singleton instance for {name}");
		}

		Instance = this;
	}
}
