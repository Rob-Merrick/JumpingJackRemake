using UnityEngine;

public class LennyManager : MonoBehaviour
{
    [SerializeField] private GameObject _lenny;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private bool _isJumping = false;
    private bool _isJumpInitialized = false;

    public static LennyManager Instance { get; private set; }

    public GameObject Lenny => _lenny;

	private void Awake()
	{
        if(Instance != null)
        {
            throw new System.Exception($"Attempting to overwrite the singleton instance for {name}");
        }

        Instance = this;
    }

	private void Start()
	{
        _spriteRenderer = _lenny.GetComponent<SpriteRenderer>();
		_animator = _lenny.GetComponent<Animator>();
	}

	private void Update()
    {
        //UpdateJump();
        UpdateMovement();
    }

	public void JumpInitialized()
	{
        _isJumpInitialized = true;
	}

    private void UpdateJump()
	{
        if(!_isJumping && Input.GetKey(KeyCode.UpArrow))
        {
            _animator.SetTrigger("Jump");
            _isJumping = true;
        }

        if(_isJumpInitialized)
		{
            //TODO: Fully implement this
            _isJumping = false;
            _isJumpInitialized = false;
		}
    }

    private void UpdateMovement()
	{
        if(!_isJumping)
        {
            bool isRightArrowDown = Input.GetKey(KeyCode.RightArrow);
            bool isLeftArrowDown = Input.GetKey(KeyCode.LeftArrow);
            bool isUpArrowDown = Input.GetKey(KeyCode.UpArrow);
            bool isDownArrowDown = Input.GetKey(KeyCode.DownArrow);

            if(isRightArrowDown && !isLeftArrowDown)
            {
                _animator.SetTrigger("Run");
                _spriteRenderer.flipX = true;
                _lenny.transform.Translate(Time.deltaTime * GameSettingsManager.Instance.RunSpeed * Vector3.right);
            }
            else if(isLeftArrowDown && !isRightArrowDown)
            {
                _animator.SetTrigger("Run");
                _spriteRenderer.flipX = false;
                _lenny.transform.Translate(Time.deltaTime * GameSettingsManager.Instance.RunSpeed * Vector3.left);
            }
            else if(isUpArrowDown && !isDownArrowDown)
            {
                _animator.SetTrigger("Run");
                _spriteRenderer.flipX = false;
                _lenny.transform.Translate(Time.deltaTime * GameSettingsManager.Instance.RunSpeed * Vector3.up);
            }
            else if(isDownArrowDown && !isUpArrowDown)
            {
                _animator.SetTrigger("Run");
                _spriteRenderer.flipX = false;
                _lenny.transform.Translate(Time.deltaTime * GameSettingsManager.Instance.RunSpeed * Vector3.down);
            }
            else
            {
                _animator.SetTrigger("Idle");
                _spriteRenderer.flipX = false;
            }
        }
    }
}
