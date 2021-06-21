using UnityEngine;

public class LennyIdleUpdate : StateMachineBehaviour
{
    private LennyManager _lennyManager;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
        _lennyManager = LennyManager.Instance;
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
        if(Input.GetKey(KeyCode.RightArrow) ^ Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.UpArrow) ^ Input.GetKey(KeyCode.DownArrow))
		{
            animator.SetOnlyTrigger("Run");
        }
    }
}
