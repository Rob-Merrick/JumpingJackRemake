using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FloorManager3D : Manager<FloorManager3D>
{
	[SerializeField] private Hole3D _holePrefab;
	[SerializeField] private DiscMesh _discPrefab;

	private readonly IDictionary<int, List<Hole3D>> _holesOnFloorLookup = new Dictionary<int, List<Hole3D>>();
	private readonly IDictionary<int, DiscMesh> _floorsNoHoles = new Dictionary<int, DiscMesh>();
	private readonly IDictionary<Hole3D, DiscMesh> _holeDiscMeshLookup = new Dictionary<Hole3D, DiscMesh>();
	private int _holeCount = 0;

	public float FloorRadius => 22.4375F;

	private void Start()
	{
		DiscMesh[] discMeshesToClear = GetComponentsInChildren<DiscMesh>();

		for(int i = 0; i < discMeshesToClear.Length; i++)
		{
			Destroy(discMeshesToClear[i].gameObject);
		}
	}

	private void Update()
	{
		UpdateHoles();
	}

	public void Restart()
	{
		_holeCount = 0;
		ClearLists();

		for(int i = 0; i < 8; i++)
		{
			_holesOnFloorLookup.Add(i, new List<Hole3D>());
			CreateNoHoleDiscMesh(i);
		}

		for(int i = 0; i < 8; i++)
		{
			SpawnHole(i);

			if(i < GameManager3D.Instance.Level - 1)
			{
				SpawnHole(i);
			}
		}
	}

	public void SpawnHole(int floorIndex)
	{
		int actualHoleIndex = _holeCount;
		_floorsNoHoles[floorIndex].gameObject.SetActive(false);
		Hole3D hole = Instantiate(_holePrefab);
		hole.HoleIndex = actualHoleIndex;
		hole.name = $"Hole{actualHoleIndex}_Floor{floorIndex}";
		hole.FloorNumber = floorIndex;
		hole.StartRotationRadians = SpawnManager3D.Instance.PickRandomFloorRotation(floorIndex);
		hole.transform.SetParent(transform, worldPositionStays: false);
		hole.MoveDirection = Random.Range(0.0F, 1.0F) <= 0.5F ? MoveAIDirection.LeftUp : MoveAIDirection.RightDown;
		hole.Initialize();
		DiscMesh newFloorMesh = Instantiate(_discPrefab);
		newFloorMesh.name = $"Disc{actualHoleIndex}_Floor{floorIndex}";
		newFloorMesh.transform.SetParent(transform);
		WarpManager3D.Instance.PlaceObjectOnFloor(newFloorMesh.gameObject, floorIndex);
		_holesOnFloorLookup[floorIndex].Add(hole);
		_holeDiscMeshLookup.Add(hole, newFloorMesh);
		_holeCount++;
	}

	private void UpdateHoles()
	{
		foreach(int floorNumber in _holesOnFloorLookup.Keys)
		{
			List<Hole3D> holes = _holesOnFloorLookup[floorNumber].OrderBy(h => h.CurrentRotation).ToList();

			for(int i = 0; i < holes.Count; i++)
			{
				Hole3D hole1 = holes[i];
				Hole3D hole2 = holes.Count > 1 ? holes[(i + 1) % holes.Count] : null;
				SetHoleSize(hole1, hole2, _holeDiscMeshLookup[hole1]);
			}
		}
	}

	private void SetHoleSize(Hole3D hole, Hole3D otherHole, DiscMesh discMesh)
	{
		discMesh.StartingRadians = hole.CurrentRotation + hole.Size;

		if(otherHole == null)
		{
			discMesh.ArcLength = 2.0F * Mathf.PI - 0.00001F - hole.Size;
		}
		else if(otherHole.CurrentRotation > hole.CurrentRotation)
		{
			discMesh.ArcLength = Mathf.Clamp(otherHole.CurrentRotation - hole.CurrentRotation - hole.Size, 0.0F, 2.0F * Mathf.PI);
		}
		else
		{
			discMesh.ArcLength = Mathf.Clamp(otherHole.CurrentRotation + 2.0F * Mathf.PI - hole.CurrentRotation - otherHole.Size, 0.0F, 2.0F * Mathf.PI);
		}

		discMesh.GetComponent<MeshRenderer>().enabled = discMesh.ArcLength > 0.0F;
	}

	private void CreateNoHoleDiscMesh(int floorNumber)
	{
		DiscMesh floorNoHoles = Instantiate(_discPrefab);
		floorNoHoles.name = $"DiscNoHoles_Floor{floorNumber}";
		floorNoHoles.ArcLength = 2.0F * Mathf.PI - 0.00001F;
		floorNoHoles.transform.SetParent(transform, worldPositionStays: false);
		WarpManager3D.Instance.PlaceObjectOnFloor(floorNoHoles.gameObject, floorNumber);
		_floorsNoHoles.Add(floorNumber, floorNoHoles);
	}

	private void ClearLists()
	{
		for(int i = 0; i < _floorsNoHoles.Count; i++)
		{
			Destroy(_floorsNoHoles[i].gameObject);
		}

		for(int i = 0; i < _holesOnFloorLookup.Keys.Count; i++)
		{
			for(int j = 0; j < _holesOnFloorLookup[i].Count; j++)
			{
				Hole3D hole = _holesOnFloorLookup[i][j];
				Destroy(hole.gameObject);
				Destroy(_holeDiscMeshLookup[hole].gameObject);
			}
		}

		_floorsNoHoles.Clear();
		_holesOnFloorLookup.Clear();
		_holeDiscMeshLookup.Clear();
	}
}
