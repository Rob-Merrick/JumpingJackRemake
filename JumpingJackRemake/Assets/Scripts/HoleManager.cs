using System.Collections.Generic;
using UnityEngine;

public class HoleManager : Manager<HoleManager>
{
	[SerializeField] private Hole _holePrefab;

	private readonly static IDictionary<int, MoveAIDirection> _moveDirectionLookup = new Dictionary<int, MoveAIDirection>()
	{
		{ 0, MoveAIDirection.LeftUp },
		{ 1, MoveAIDirection.RightDown },
		{ 2, MoveAIDirection.RightDown },
		{ 3, MoveAIDirection.RightDown },
		{ 4, MoveAIDirection.RightDown },
		{ 5, MoveAIDirection.LeftUp },
		{ 6, MoveAIDirection.LeftUp },
		{ 7, MoveAIDirection.LeftUp },
	};

	private readonly List<Hole> _holes = new List<Hole>();
	private readonly List<Hole> _inactiveHoles = new List<Hole>();

	public Hole[] Holes => _holes.ToArray();

	public void Restart()
	{
		foreach(Hole hole in Holes)
		{
			Destroy(hole.gameObject);
		}

		foreach(Hole hole in _inactiveHoles)
		{
			Destroy(hole.gameObject);
		}

		_holes.Clear();
		_inactiveHoles.Clear();
		Initialize();
	}

	public void SpawnHole()
	{
		if(_inactiveHoles.Count > 0)
		{
			Hole holeToSpawn = _inactiveHoles[0];
			_inactiveHoles.RemoveAt(0);
			holeToSpawn.GetComponent<MoveAI>().IsSpawned = true;
			_holes.Add(holeToSpawn);
		}
	}

	//This works by spawning the maximum number of holes from the beginning so that they can all maintain the right distance away from each other.
	//Holes not "spawned" yet are just hidden until SpawnHole is called.
	private void Initialize()
	{
		for(int i = 0; i < 8; i++)
		{
			MoveAIDirection moveDirection = _moveDirectionLookup[i];
			Hole hole = Instantiate(_holePrefab);
			MoveAI holeMovement = hole.GetComponent<MoveAI>();
			hole.gameObject.transform.SetParent(transform, worldPositionStays: false);
			holeMovement.MoveDirection = moveDirection;
			holeMovement.IsSpawned = false;
			_inactiveHoles.Add(hole);
		}

		SpawnHole();
		SpawnHole();
	}
}
