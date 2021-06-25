using UnityEngine;

public class HoleManager3D : Manager<HoleManager3D>
{
	[SerializeField] [Range(0.0F, 2.0F * Mathf.PI)] private float _holeSizeRadians = 0.3F;
	[SerializeField] [Range(0.0F, 2.0F * Mathf.PI)] private float _rotationalSpeedRadians = 2.0F * Mathf.PI / 3.0F;

	public float HoleSizeRadians => _holeSizeRadians;
	public float RotationalSpeedRadians => _rotationalSpeedRadians;
}