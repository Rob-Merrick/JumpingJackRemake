using System.Collections.Generic;
using UnityEngine;

public class HoleManager : MonoBehaviour
{
    public static HoleManager Instance { get; private set; }

	private readonly List<Hole> _holes = new List<Hole>();

	public Hole[] Holes => _holes.ToArray();

	private void Awake()
	{
		if(Instance != null)
		{
			throw new System.Exception($"Attempting to overwrite the singleton instance for {name}");
		}

		Instance = this;
	}

	private void Start()
	{
		foreach(Hole hole in FindObjectsOfType<Hole>())
		{
			_holes.Add(hole);
		}
	}
}
