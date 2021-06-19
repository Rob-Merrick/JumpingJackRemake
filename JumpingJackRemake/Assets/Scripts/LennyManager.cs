using UnityEngine;

public class LennyManager : MonoBehaviour
{
    [SerializeField] private GameObject _lenny;
    [SerializeField] [Range(0.1F, 10.0F)] private float _runSpeed = 1.0F;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private bool _isJumping = false;

	private void Start()
	{
        _spriteRenderer = _lenny.GetComponent<SpriteRenderer>();
		_animator = _lenny.GetComponent<Animator>();
	}

	private void Update()
    {
        bool isRightArrowDown = Input.GetKey(KeyCode.RightArrow);
        bool isLeftArrowDown = Input.GetKey(KeyCode.LeftArrow);

        if(Input.GetKey(KeyCode.UpArrow))
		{
            _animator.SetTrigger("Jump");
            _isJumping = true;
		}

        if(!_isJumping)
		{
            if(isRightArrowDown && !isLeftArrowDown)
            {
                _animator.SetTrigger("Run");
                _spriteRenderer.flipX = true;
                _lenny.transform.Translate(Time.deltaTime * _runSpeed * Vector3.right);
            }
            else if(isLeftArrowDown && !isRightArrowDown)
            {
                _animator.SetTrigger("Run");
                _spriteRenderer.flipX = false;
                _lenny.transform.Translate(Time.deltaTime * _runSpeed * Vector3.left);
            }
            else
            {
                _animator.SetTrigger("Idle");
                _spriteRenderer.flipX = false;
            }
        }
    }
}
