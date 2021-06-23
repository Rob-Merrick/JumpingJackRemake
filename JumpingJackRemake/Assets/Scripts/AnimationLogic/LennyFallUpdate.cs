using UnityEngine;

public class LennyFallUpdate : StateMachineBehaviour
{
    private LennyManager _lennyManager;
	private GameObject _fallTarget;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
        _lennyManager = LennyManager.Instance;
		_fallTarget = new GameObject("LennyFallTarget");
		_fallTarget.transform.position = _lennyManager.LennyGameObject.transform.position;
		int targetFloor = _lennyManager.HitHead ? _lennyManager.FloorNumber : _lennyManager.FloorNumber - 1;
		WarpManager.Instance.PlaceObjectOnFloor(_fallTarget, targetFloor, _lennyManager.FloorOffset);
		AudioSource fallSound = SoundManager.Instance.GetAudioSourceByName("FallDown");
		fallSound.Play();
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		_lennyManager.LennyGameObject.transform.Translate(Time.deltaTime * _lennyManager.FallSpeed * Vector3.down);

		if(Vector3.Distance(_lennyManager.LennyGameObject.transform.position, _fallTarget.transform.position) <= 1.0F)
		{
			_lennyManager.FinishFalling();
			animator.SetOnlyTrigger("Stun");
		}
	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		AudioSource fallSound = SoundManager.Instance.GetAudioSourceByName("FallDown");
		fallSound.Stop();
		Destroy(_fallTarget);
	}
}
