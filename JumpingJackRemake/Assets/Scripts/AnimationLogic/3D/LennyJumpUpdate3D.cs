using UnityEngine;

public class LennyJumpUpdate3D : StateMachineBehaviour
{
	private LennyManager3D _lennyManager;
	private float _upwardMovement;
	private float _upwardAcceleration;
	private float _startingHeight;
	private bool _isHoldingJumpButton;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		_lennyManager = LennyManager3D.Instance;
		_lennyManager.IsJumping = true;
		_lennyManager.JumpBeginVerticalAscent = false;
		_lennyManager.JumpApex = false;
		_lennyManager.JumpEndVerticalDescent = false;
		_upwardMovement = 0.0F;
		_upwardAcceleration = 0.0F;
		_startingHeight = _lennyManager.Lenny.transform.position.y;
		_isHoldingJumpButton = Input.GetButton("Jump");
		SoundManager3D.Instance.PlaySound(Random.Range(0.0F, 1.0F) < 0.3333F ? "Jump1" : Random.Range(0.0F, 1.0F) < 0.5F ? "Jump2" : "Jump3");
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if(!_lennyManager.JumpBeginVerticalAscent && !_lennyManager.JumpApex && !_lennyManager.JumpEndVerticalDescent)
		{
			_isHoldingJumpButton = _isHoldingJumpButton && Input.GetButton("Jump");

			if(_isHoldingJumpButton)
			{
				_upwardAcceleration = Mathf.Lerp(100.0F, 400.0F, stateInfo.normalizedTime / (19.0F / 65.0F));
			}
		}
		else if(_lennyManager.IsHeadCollided)
		{
			_lennyManager.Animator.SetOnlyTrigger("SmashHead");
		}
		else
		{
			_upwardMovement += _upwardAcceleration * Time.deltaTime;
			_upwardAcceleration -= 75.0F * _lennyManager.GravityAcceleration * Time.deltaTime;
			_lennyManager.CharacterController.Move(_upwardMovement * Time.deltaTime * Vector3.up);
			_lennyManager.ApplyUserMovement();

			if(_lennyManager.JumpApex)
			{
				_lennyManager.Animator.speed = 0.5F;
			}

			if(_lennyManager.CharacterController.isGrounded)
			{
				_lennyManager.Animator.speed = 3.0F;
				_lennyManager.Animator.SetOnlyTrigger("Idle");
			}
			else if(_lennyManager.Lenny.transform.position.y < _startingHeight)
			{
				_lennyManager.Animator.speed = 3.0F;
				_lennyManager.Animator.SetOnlyTrigger("Falling");
			}
		}
	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		_lennyManager.IsJumping = false;
		_lennyManager.JumpBeginVerticalAscent = false;
		_lennyManager.JumpApex = false;
		_lennyManager.JumpEndVerticalDescent = false;
		_lennyManager.Animator.speed = 1.0F;
	}
}
