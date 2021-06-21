using System.Collections.Generic;
using UnityEngine;

public class HoleManager : MonoBehaviour
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
