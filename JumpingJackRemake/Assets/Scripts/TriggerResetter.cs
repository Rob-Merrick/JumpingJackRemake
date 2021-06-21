using UnityEngine;

public class TriggerResetter : StateMachineBehaviour
{
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		foreach(AnimatorControllerParameter parameter in animator.parameters)
		{
			if(parameter.type == AnimatorControllerParameterType.Trigger)
			{
				animator.ResetTrigger(parameter.name);
			}
		}
	}
}
