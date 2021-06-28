using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager3D : Manager<GameManager3D>
{
	public bool IsReady { get; private set; } = false;

	private bool _tempIsRestarted = false;

	private void Update()
	{
		if(!_tempIsRestarted)
		{
			Restart();
			_tempIsRestarted = true;
		}

		if(Input.GetKeyDown(KeyCode.Space))
		{
			IsReady = true;
		}
	}

	private void Restart()
	{
		SpawnManager3D.Instance.Restart();
		FloorManager3D.Instance.Restart();
		HazardManager3D.Instance.Restart();
		LennyManager3D.Instance.Restart();
	}
}
