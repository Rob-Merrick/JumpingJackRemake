using UnityEngine;

public static class AnimatorExtensions
{
	public static void SetOnlyTrigger(this Animator animator, string trigger)
	{
		foreach(AnimatorControllerParameter parameter in animator.parameters)
		{
			if(parameter.type == AnimatorControllerParameterType.Trigger)
			{
				animator.ResetTrigger(parameter.name);
			}
		}

		animator.SetTrigger(trigger);
	}
}
