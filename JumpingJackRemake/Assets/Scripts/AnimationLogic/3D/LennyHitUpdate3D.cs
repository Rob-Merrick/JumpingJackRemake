using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LennyHitUpdate3D : StateMachineBehaviour
{
	private LennyManager3D _lennyManager;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		_lennyManager = LennyManager3D.Instance;
		_lennyManager.IsHit = true;
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if(!_lennyManager.CharacterController.isGrounded)
		{
			_lennyManager.Animator.SetOnlyTrigger("Falling");
		}
	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		_lennyManager.IsHit = false;
	}
}
