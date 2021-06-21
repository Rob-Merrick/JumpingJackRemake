using UnityEngine;

public class LennyIdleUpdate : StateMachineBehaviour
{
    private LennyManager _lennyManager;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
        _lennyManager = LennyManager.Instance;
		WarpManager.Instance.PlaceObjectOnFloor(_lennyManager.LennyGameObject, _lennyManager.FloorNumber, _lennyManager.FloorOffset);
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if(_lennyManager.IsGoingToFall())
		{
			animator.SetOnlyTrigger("Fall");
		}
        else if(Input.GetKey(KeyCode.RightArrow) ^ Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.UpArrow) ^ Input.GetKey(KeyCode.DownArrow))
		{
            animator.SetOnlyTrigger("Run");
        }
		else if(Input.GetKey(KeyCode.UpArrow))
		{
			animator.SetOnlyTrigger("Jump");
		}
    }
}
