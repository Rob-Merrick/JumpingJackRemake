using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager3D : Manager<CameraManager3D>
{
	[SerializeField] private Lenny3D _lenny;
	[SerializeField] private GameObject _dollyTrack;

	private void Update()
	{
		if(LennyManager3D.Instance.Lenny.transform.position.y >= 0)
		{
			_dollyTrack.transform.position = new Vector3(_dollyTrack.transform.position.x, _lenny.transform.position.y, _dollyTrack.transform.position.z);
		}
	}
}
