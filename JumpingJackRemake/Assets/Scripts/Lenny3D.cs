using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lenny3D : MonoBehaviour
{
	public CharacterController CharacterController { get; private set; }

	private void Start()
	{
		CharacterController = GetComponent<CharacterController>();
	}
}
