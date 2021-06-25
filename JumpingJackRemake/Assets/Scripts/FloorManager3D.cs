using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FloorManager3D : Manager<FloorManager3D>
{
	[SerializeField] private Floor3D _firstFloor;
	[SerializeField] private Floor3D _secondFloor;
	[SerializeField] private Floor3D _thirdFloor;
	[SerializeField] private Floor3D _fourthFloor;
	[SerializeField] private Floor3D _fifthFloor;
	[SerializeField] private Floor3D _sixthFloor;
	[SerializeField] private Floor3D _seventhFloor;
	[SerializeField] private Floor3D _eigthFloor;

	[SerializeField] private Hole3D _holePrefab;
	[SerializeField] private DiscMesh _discPrefab;

	private DiscMesh _floorNoHoles;
	private List<Hole3D> _holes = new List<Hole3D>();
	private readonly IDictionary<Hole3D, DiscMesh> _holeDiscMeshLookup = new Dictionary<Hole3D, DiscMesh>();

	private float HoleSize => HoleManager3D.Instance.HoleSizeRadians;

	private void Start()
	{
		_floorNoHoles = Instantiate(_discPrefab);
		_floorNoHoles.ArcLength = 2.0F * Mathf.PI - 0.00001F;
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(_floorNoHoles != null)
			{
				Destroy(_floorNoHoles.gameObject);
			}
			
			Hole3D hole = Instantiate(_holePrefab);
			hole.StartRotationRadians = Random.Range(0.0F, 2.0F * Mathf.PI);
			hole.transform.SetParent(transform, worldPositionStays: false);
			hole.MoveDirection = Random.Range(0.0F, 1.0F) <= 0.5F ? MoveAIDirection.LeftUp : MoveAIDirection.RightDown;
			_holes.Add(hole);
			_holeDiscMeshLookup.Add(hole, Instantiate(_discPrefab));
		}

		_holes = _holes.OrderBy(h => h.CurrentRotation).ToList();

		for(int i = 0; i < _holes.Count; i++)
		{
			Hole3D hole1 = _holes[i];
			Hole3D hole2 = _holes.Count > 1 ? _holes[(i + 1) % _holes.Count] : null;
			SetHoleSize(hole1, hole2, _holeDiscMeshLookup[hole1]);
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
