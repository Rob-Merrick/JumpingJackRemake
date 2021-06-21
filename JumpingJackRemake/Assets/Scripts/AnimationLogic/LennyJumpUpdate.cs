using UnityEngine;

public class LennyJumpUpdate : StateMachineBehaviour
{
    private LennyManager _lennyManager;
	private GameObject _jumpTarget;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
        _lennyManager = LennyManager.Instance;
		_jumpTarget = new GameObject("LennyJumpTarget");
		_jumpTarget.transform.position = _lennyManager.LennyGameObject.transform.position;
		WarpManager.Instance.PlaceObjectOnFloor(_jumpTarget, _lennyManager.FloorNumber + 1, _lennyManager.FloorOffset);
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if(_lennyManager.JumpInitialized)
		{
			_lennyManager.LennyGameObject.transform.Translate(Time.deltaTime * _lennyManager.JumpSpeed * Vector3.up);

			if(_lennyManager.HitHead)
			{
				animator.SetOnlyTrigger("HitHead");
			}
			else if(Vector3.Distance(_lennyManager.LennyGameObject.transform.position, _jumpTarget.transform.position) <= 1.0F)
			{
				_lennyManager.FinishJumping();
				animator.SetOnlyTrigger("Idle");
			}
		}
    }

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		_lennyManager.JumpInitialized = false;
		Destroy(_jumpTarget);
	}
}
