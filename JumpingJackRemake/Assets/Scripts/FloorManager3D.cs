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

	private float HoleSize => HoleManager3D.Instance.HoleSizeRadians;

	public float FloorRadius => 22.4375F;

	private void Start()
	{
		DiscMesh[] discMeshesToClear = GetComponentsInChildren<DiscMesh>();

		for(int i = 0; i < discMeshesToClear.Length; i++)
		{
			Destroy(discMeshesToClear[i].gameObject);
		}

		Restart(); //TODO: When the game manager exists, get rid of this call. The game manager should be in charge of this.
	}

	private void Update()
	{
		UpdateHoles();
	}

	public void Restart()
	{
		for(int i = 0; i < _floorsNoHoles.Count; i++)
		{
			Destroy(_floorsNoHoles[i].gameObject);
		}

		_floorsNoHoles.Clear();

		for(int i = 0; i < _holesOnFloorLookup.Keys.Count; i++)
		{
			for(int j = 0; j < _holesOnFloorLookup[i].Count; j++)
			{
				Destroy(_holesOnFloorLookup[i][j].gameObject);
			}
		}

		_holesOnFloorLookup.Clear();
		_holeCount = 0;

		for(int i = 0; i < 8; i++)
		{
			DiscMesh floorNoHoles = Instantiate(_discPrefab);
			floorNoHoles.ArcLength = 2.0F * Mathf.PI - 0.00001F;
			floorNoHoles.transform.SetParent(transform, worldPositionStays: false);
			WarpManager3D.Instance.PlaceObjectOnFloor(floorNoHoles.gameObject, i);
			_floorsNoHoles.Add(i, floorNoHoles);
		}

		_holeDiscMeshLookup.Clear();

		for(int i = 0; i < 8; i++)
		{
			SpawnHole();
		}

		//SpawnHole();
	}

	public void SpawnHole()
	{
		if(_holeCount < 8)
		{
			int floorNumber = Random.Range(0, 8);

			if(_floorsNoHoles.TryGetValue(floorNumber, out DiscMesh floorNoHole))
			{
				Destroy(floorNoHole.gameObject);
			}

			Hole3D hole = Instantiate(_holePrefab);
			hole.FloorNumber = floorNumber;
			hole.StartRotationRadians = Random.Range(0.0F, 2.0F * Mathf.PI);
			hole.transform.SetParent(transform, worldPositionStays: false);
			hole.MoveDirection = Random.Range(0.0F, 1.0F) <= 0.5F ? MoveAIDirection.LeftUp : MoveAIDirection.RightDown;
			DiscMesh newFloorMesh = Instantiate(_discPrefab);
			WarpManager3D.Instance.PlaceObjectOnFloor(newFloorMesh.gameObject, floorNumber);

			if(!_holesOnFloorLookup.ContainsKey(floorNumber))
			{
				_holesOnFloorLookup.Add(floorNumber, new List<Hole3D>());
			}

			_holesOnFloorLookup[floorNumber].Add(hole);
			_holeDiscMeshLookup.Add(hole, newFloorMesh);
			_holeCount++;
		}
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
		discMesh.StartingRadians = hole.CurrentRotation + HoleSize;

		if(otherHole == null)
		{
			discMesh.ArcLength = 2.0F * Mathf.PI - 0.00001F - HoleSize;
		}
		else if(otherHole.CurrentRotation > hole.CurrentRotation)
		{
			discMesh.ArcLength = Mathf.Clamp(otherHole.CurrentRotation - hole.CurrentRotation - HoleSize, 0.0F, 2.0F * Mathf.PI);
		}
		else
		{
			discMesh.ArcLength = Mathf.Clamp(otherHole.CurrentRotation + 2.0F * Mathf.PI - hole.CurrentRotation - HoleSize, 0.0F, 2.0F * Mathf.PI);
		}

		discMesh.GetComponent<MeshRenderer>().enabled = discMesh.ArcLength > 0.0F;
	}
}
