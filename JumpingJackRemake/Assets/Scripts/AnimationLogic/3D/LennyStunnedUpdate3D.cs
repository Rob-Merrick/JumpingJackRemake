using UnityEngine;

public class LennyStunnedUpdate3D : StateMachineBehaviour
{
	private LennyManager3D _lennyManager;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		_lennyManager = LennyManager3D.Instance;
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if(GameManager3D.Instance.IsReady && !_lennyManager.CharacterController.isGrounded)
		{
			_lennyManager.Animator.SetOnlyTrigger("Falling");
		}
	}
}
