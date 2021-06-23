using System;
using UnityEngine;

public class LennyRunUpdate : StateMachineBehaviour
{
    private LennyManager _lennyManager;
    private bool _isOnScreenLeftEdge;
    private bool _isOnScreenRightEdge;
    private float? _forcedWarpTagetX;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
        _lennyManager = LennyManager.Instance;
        _isOnScreenLeftEdge = false;
        _isOnScreenRightEdge = false;
        _forcedWarpTagetX = null;
        WarpManager.Instance.PlaceObjectOnFloor(_lennyManager.LennyGameObject, _lennyManager.FloorNumber, _lennyManager.FloorOffset);
        AudioSource runSound = SoundManager.Instance.GetAudioSourceByName("Run");
        runSound.Play();
    }

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
        bool isRightArrowDown = Input.GetKey(KeyCode.RightArrow);
        bool isLeftArrowDown = Input.GetKey(KeyCode.LeftArrow);

        if(_isOnScreenLeftEdge)
		{
            ForceRun(ref _isOnScreenLeftEdge, RunLeft);
        }
        else if(_isOnScreenRightEdge)
		{
            ForceRun(ref _isOnScreenRightEdge, RunRight);
        }
        else if(_lennyManager.LennyGameObject.transform.position.x <= ScreenManager.Instance.PlayableAreaLeftEdge)
		{
            _forcedWarpTagetX = ScreenManager.Instance.PlayableAreaRightEdge - 8.0F;
            _isOnScreenLeftEdge = true;
        }
        else if(_lennyManager.LennyGameObject.transform.position.x >= ScreenManager.Instance.PlayableAreaRightEdge)
		{
            _forcedWarpTagetX = ScreenManager.Instance.PlayableAreaLeftEdge + 8.0F;
            _isOnScreenRightEdge = true;
        }
        else if(_lennyManager.IsGoingToFall())
        {
            animator.SetOnlyTrigger("Fall");
        }
        else if(isRightArrowDown && !isLeftArrowDown)
        {
            RunRight();
        }
        else if(isLeftArrowDown && !isRightArrowDown)
        {
            RunLeft();
        }
        else if(Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetOnlyTrigger("Jump");
        }
        else
        {
            animator.SetOnlyTrigger("Idle");
            _lennyManager.SpriteRenderer.flipX = false;
        }
    }

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
        AudioSource runSound = SoundManager.Instance.GetAudioSourceByName("Run");
        runSound.Stop();
    }

	private void RunLeft()
	{
        _lennyManager.SpriteRenderer.flipX = false;
        _lennyManager.LennyGameObject.transform.Translate(Time.deltaTime * _lennyManager.LennySpeed * Vector3.left);
    }

    private void RunRight()
	{
        _lennyManager.SpriteRenderer.flipX = true;
        _lennyManager.LennyGameObject.transform.Translate(Time.deltaTime * _lennyManager.LennySpeed * Vector3.right);
    }

    private void ForceRun(ref bool directionTrigger, Action runMethod)
	{
        runMethod.Invoke();

        if(Mathf.Abs(_lennyManager.LennyGameObject.transform.position.x - _forcedWarpTagetX.Value) < 2.0F)
        {
            directionTrigger = false;
        }
    }
}
