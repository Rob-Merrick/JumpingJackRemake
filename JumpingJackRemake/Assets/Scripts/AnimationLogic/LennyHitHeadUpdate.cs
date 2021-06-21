using UnityEngine;

public class LennyHitHeadUpdate : StateMachineBehaviour
{
	private LennyManager _lennyManager;
	private float _previousTimeScale;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		_lennyManager = LennyManager.Instance;
		_previousTimeScale = Time.timeScale;
		Time.timeScale = 0.15F;
	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		Time.timeScale = _previousTimeScale;
	}
}
