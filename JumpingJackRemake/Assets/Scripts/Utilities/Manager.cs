using UnityEngine;

public class Manager<T> : MonoBehaviour where T : Manager<T>
{
    public static T Instance { get; private set; }

    private void Awake()
    {
        //if(Instance != null)
        //{
        //    throw new System.Exception($"Attempting to overwrite the singleton instance for {name}");
        //}

        Instance = (T) this;
    }
}
