using UnityEngine;

public class HazardAnimationPicker : StateMachineBehaviour
{
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.SetTrigger(animator.gameObject.GetComponent<Hazard>().HazardType.ToString());
	}
}
