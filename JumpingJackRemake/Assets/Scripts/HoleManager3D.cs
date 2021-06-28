using UnityEngine;

public class HoleManager3D : Manager<HoleManager3D>
{
	[SerializeField] [Range(0.0F, 2.0F * Mathf.PI)] private float _holeSizeRadians = 0.3F;
	[SerializeField] [Range(0.0F, 2.0F * Mathf.PI)] private float _rotationalSpeedRadians = 2.0F * Mathf.PI / 3.0F;
	[SerializeField] [Range(0.01F, 10.0F)] private float _growShrinkTime = 2.0F;

	public float HoleSizeRadians => _holeSizeRadians;
	public float RotationalSpeedRadians => GameManager3D.Instance.IsReady ? _rotationalSpeedRadians : 0.0F;
	public float GrowShrinkTime => _growShrinkTime;
}
