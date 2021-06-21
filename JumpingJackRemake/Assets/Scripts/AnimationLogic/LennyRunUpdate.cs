using UnityEngine;

public class LennyRunUpdate : StateMachineBehaviour
{
    private LennyManager _lennyManager;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
        _lennyManager = LennyManager.Instance;
        WarpManager.Instance.PlaceObjectOnFloor(_lennyManager.LennyGameObject, _lennyManager.FloorNumber, _lennyManager.FloorOffset);
    }

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
        bool isRightArrowDown = Input.GetKey(KeyCode.RightArrow);
        bool isLeftArrowDown = Input.GetKey(KeyCode.LeftArrow);

        if(_lennyManager.IsGoingToFall())
        {
            animator.SetOnlyTrigger("Fall");
        }
        else if(isRightArrowDown && !isLeftArrowDown)
        {
            _lennyManager.SpriteRenderer.flipX = true;
            _lennyManager.LennyGameObject.transform.Translate(Time.deltaTime * _lennyManager.LennySpeed * Vector3.right);
        }
        else if(isLeftArrowDown && !isRightArrowDown)
        {
            _lennyManager.SpriteRenderer.flipX = false;
            _lennyManager.LennyGameObject.transform.Translate(Time.deltaTime * _lennyManager.LennySpeed * Vector3.left);
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
}
