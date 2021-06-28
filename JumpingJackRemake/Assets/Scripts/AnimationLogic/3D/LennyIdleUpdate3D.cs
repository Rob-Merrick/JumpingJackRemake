using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LennyIdleUpdate3D : StateMachineBehaviour
{
	private LennyManager3D _lennyManager;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		_lennyManager = LennyManager3D.Instance;
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if(!GameManager3D.Instance.IsReady)
		{
			return;
		}

		if(!_lennyManager.CharacterController.isGrounded)
		{
			_lennyManager.Animator.SetOnlyTrigger("Falling");
		}
		else if(Mathf.Abs(Input.GetAxis("Horizontal")) > GameSettingsManager3D.Instance.HoriztonalInputAxisDeadzone)
		{
			_lennyManager.Animator.SetOnlyTrigger("Running");
		}
		else if(Input.GetButton("Jump"))
		{
			_lennyManager.Animator.SetOnlyTrigger("Jumping");
		}
	}
}
