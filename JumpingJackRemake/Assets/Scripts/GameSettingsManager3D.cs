using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsManager3D : Manager<GameSettingsManager3D>
{
	[SerializeField] [Range(0.0F, 1.0F)] private float _horizontalInputAxisDeadzone = 0.1F;

	public float HoriztonalInputAxisDeadzone => _horizontalInputAxisDeadzone;
}
