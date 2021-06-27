using UnityEngine;

public class LennySmashHeadUpdate3D : StateMachineBehaviour
{
	private LennyManager3D _lennyManager;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		_lennyManager = LennyManager3D.Instance;
		_lennyManager.IsSmashingHead = true;
	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		_lennyManager.IsSmashingHead = false;
	}
}
