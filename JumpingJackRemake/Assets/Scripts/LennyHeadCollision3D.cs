using System.Collections.Generic;
using UnityEngine;

public class LennyHeadCollision3D : MonoBehaviour
{
	private readonly List<Collider> _collisions = new List<Collider>();

	public bool IsHeadCollidedWithCeiling => _collisions.Count > 0;

	private void LateUpdate()
	{
		LennyManager3D.Instance.IsHeadCollided = IsHeadCollidedWithCeiling;
		_collisions.Clear();
	}

	private void OnTriggerStay(Collider other)
	{
		if(other.GetComponent<DiscMesh>() != null)
		{
			_collisions.Add(other);
		}
	}
}
