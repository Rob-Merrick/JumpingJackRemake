using UnityEngine;

public class HazardManager : MonoBehaviour
{
    public static HazardManager Instance;

	private void Awake()
	{
		if(Instance != null)
		{
			throw new System.Exception($"Attemption to overwrite the singleton instance for {name}");
		}

		Instance = this;
	}
}
