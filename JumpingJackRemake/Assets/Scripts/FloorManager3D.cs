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

	[SerializeField] private DiscMesh _disc1;
	[SerializeField] private DiscMesh _disc2;
	[SerializeField] private Hole3D _hole1;
	[SerializeField] private Hole3D _hole2;

	private float HoleSize => HoleManager3D.Instance.HoleSizeRadians;

	private void Update()
	{
		SetHoleSize(_hole1, _hole2, _disc1);
		SetHoleSize(_hole2, _hole1, _disc2);
	}

	//discMesh.ArcLength = 2.0F * Mathf.PI; //This is the fallback for no other holes.
	private void SetHoleSize(Hole3D hole, Hole3D otherHole, DiscMesh discMesh)
	{
		discMesh.StartingRadians = hole.CurrentRotation + HoleSize;

		if(otherHole.CurrentRotation > hole.CurrentRotation)
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
