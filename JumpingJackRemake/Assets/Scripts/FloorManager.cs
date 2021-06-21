using UnityEngine;

public class FloorManager : MonoBehaviour
{
    public static FloorManager Instance;

	private void Awake()
	{
		if(Instance != null)
		{
			throw new System.Exception($"Attemption to overwrite the singleton instance for {name}");
		}

		Instance = this;
	}
}
