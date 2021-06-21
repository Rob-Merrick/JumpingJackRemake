using System.Collections.Generic;
using UnityEngine;

public class HoleManager : Manager<HoleManager>
{
	[SerializeField] private Hole _holePrefab;

	private static IDictionary<int, MoveAIDirection> _moveDirectionLookup = new Dictionary<int, MoveAIDirection>()
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

	public Hole[] Holes => _holes.ToArray();

	private void Start()
	{
		SpawnHole();
		SpawnHole();
	}

	public void SpawnHole()
	{
		if(_holes.Count < 8)
		{
			MoveAIDirection moveDirection = _moveDirectionLookup[_holes.Count];
			Hole hole = Instantiate(_holePrefab);
			hole.gameObject.transform.SetParent(transform, worldPositionStays: false);
			hole.GetComponent<MoveAI>().MoveDirection = moveDirection;
			_holes.Add(hole);
		}
	}
}
