using UnityEngine;

public class LennyStunUpdate : StateMachineBehaviour
{
    private LennyManager _lennyManager;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
        _lennyManager = LennyManager.Instance;
		WarpManager.Instance.PlaceObjectOnFloor(_lennyManager.LennyGameObject, _lennyManager.FloorNumber, _lennyManager.FloorOffset);
		AudioSource stunSound = SoundManager.Instance.GetAudioSourceByName("Stunned");
		stunSound.Play();
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.SetFloat("StunTime", animator.GetFloat("StunTime") + Time.deltaTime);

		if(_lennyManager.IsGoingToFall())
		{
			animator.SetOnlyTrigger("Fall");
		}
		else if(animator.GetFloat("StunTime") >= _lennyManager.StunTime)
		{
			animator.SetOnlyTrigger("Idle");
		}
	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.SetFloat("StunTime", 0.0F);
		AudioSource stunSound = SoundManager.Instance.GetAudioSourceByName("Stunned");
		stunSound.Stop();
	}
}
