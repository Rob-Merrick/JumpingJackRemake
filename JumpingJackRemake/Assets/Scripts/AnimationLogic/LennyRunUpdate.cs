using UnityEngine;

public class LennyRunUpdate : StateMachineBehaviour
{
    private LennyManager _lennyManager;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
        _lennyManager = LennyManager.Instance;
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
        bool isRightArrowDown = Input.GetKey(KeyCode.RightArrow);
        bool isLeftArrowDown = Input.GetKey(KeyCode.LeftArrow);
        bool isUpArrowDown = Input.GetKey(KeyCode.UpArrow);
        bool isDownArrowDown = Input.GetKey(KeyCode.DownArrow);

        if(isRightArrowDown && !isLeftArrowDown)
        {
            _lennyManager.SpriteRenderer.flipX = true;
            _lennyManager.LennyGameObject.transform.Translate(Time.deltaTime * _lennyManager.LennySpeed * Vector3.right);
        }
        else if(isLeftArrowDown && !isRightArrowDown)
        {
            _lennyManager.SpriteRenderer.flipX = false;
            _lennyManager.LennyGameObject.transform.Translate(Time.deltaTime * _lennyManager.LennySpeed * Vector3.left);
        }
        else if(isUpArrowDown && !isDownArrowDown)
        {
            _lennyManager.SpriteRenderer.flipX = false;
            _lennyManager.LennyGameObject.transform.Translate(Time.deltaTime * _lennyManager.LennySpeed * Vector3.up);
        }
        else if(isDownArrowDown && !isUpArrowDown)
        {
            _lennyManager.SpriteRenderer.flipX = false;
            _lennyManager.LennyGameObject.transform.Translate(Time.deltaTime * _lennyManager.LennySpeed * Vector3.down);
        }
        else
        {
            animator.SetOnlyTrigger("Idle");
            _lennyManager.SpriteRenderer.flipX = false;
        }
    }
}
