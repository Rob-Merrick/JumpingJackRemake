using UnityEngine;

public class LennyManager : MonoBehaviour
{
    [SerializeField] private GameObject _lenny;
    [SerializeField] private int _floorOffset = -1;
    [SerializeField] private float _lennySpeedDifference = 10.0F;
    [SerializeField] [Range(0.0F, 5.0F)] private float _stunTime = 2.0F;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private bool _isJumping = false;
    private bool _isJumpInitialized = false;
    private int _floorNumber = 0;

    public static LennyManager Instance { get; private set; }

    public float LennySpeed => GameSettingsManager.Instance.RunSpeed + _lennySpeedDifference;
    public float StunTime => _stunTime;
    public SpriteRenderer SpriteRenderer => _spriteRenderer;
    public GameObject LennyGameObject => _lenny;

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
#if UNITY_EDITOR
        UpdateTestingShortcuts();
#endif
        //UpdateJump();
    }

    //TODO: Re-evaluate this. There's most-likely a better approach to take
	public void JumpInitialized()
	{
        _isJumpInitialized = true;
	}

    public void Stun()
	{
        _animator.SetFloat("StunTime", 0.0F);
        _animator.SetOnlyTrigger("Stun");
	}

    //TODO: Move this to the animation behavior file. It doesn't belong here.
    private void UpdateJump()
	{
        if(!_isJumping && Input.GetKey(KeyCode.UpArrow))
        {
            _animator.SetOnlyTrigger("Jump");
            _isJumping = true;
        }

        if(_isJumpInitialized)
		{
            //TODO: Fully implement this
            _isJumping = false;
            _isJumpInitialized = false;
		}
    }

#if UNITY_EDITOR
    private void UpdateTestingShortcuts()
	{
        if(Input.GetKeyDown(KeyCode.Equals) || Input.GetKeyDown(KeyCode.KeypadPlus))
		{
            _floorNumber++;

            if(_floorNumber > 7)
			{
                _floorNumber = 0;
			}

            WarpManager.Instance.PlaceObjectOnFloor(LennyGameObject, _floorNumber, _floorOffset);
		}
        else if(Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            _floorNumber--;

            if(_floorNumber < 0)
            {
                _floorNumber = 7;
            }

            WarpManager.Instance.PlaceObjectOnFloor(LennyGameObject, _floorNumber, _floorOffset);
        }
    }
#endif
}
