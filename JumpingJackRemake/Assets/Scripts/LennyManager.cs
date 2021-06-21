using UnityEngine;

public class LennyManager : MonoBehaviour
{
    [SerializeField] private GameObject _lenny;
    [SerializeField] private int _floorOffset = -1;
    [SerializeField] private float _lennySpeedDifference = 10.0F;
    [SerializeField] [Range(1.0F, 100.0F)] private float _fallSpeed = 50.0F;
    [SerializeField] [Range(1.0F, 100.0F)] private float _jumpSpeed = 75.0F;
    [SerializeField] [Range(0.0F, 5.0F)] private float _stunTime = 2.0F;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    public static LennyManager Instance { get; private set; }

    public bool JumpInitialized { get; set; } = false;
    public bool JumpIsGood { get; set; } = false;
    public bool HitHead { get; set; } = false;
    public int RemainingLives { get; private set; } = 6;
    public int ActiveHoles { get; private set; } = 0;
    public int FloorNumber { get; private set; } = 0;
    public int FloorOffset => _floorOffset;
    public float LennySpeed => GameSettingsManager.Instance.RunSpeed + _lennySpeedDifference;
    public float StunTime => _stunTime;
    public float FallSpeed => _fallSpeed;
    public float JumpSpeed => _jumpSpeed;
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

#if UNITY_EDITOR
	private void Update()
    {
        UpdateTestingShortcuts();
    }
#endif

    public void AddActiveHole()
	{
        ActiveHoles++;
	}

    public void RemoveActiveHole()
	{
        ActiveHoles--;
	}

    public bool IsGoingToFall()
	{
        return !JumpIsGood && ActiveHoles > 0;
	}

    public void FinishFalling()
	{
        ChangeFloors(isUp: false, isCheatButton: false);
        HitHead = false;
	}

    public void FinishJumping()
	{
        JumpIsGood = true;
        JumpInitialized = false;
        ChangeFloors(isUp: true, isCheatButton: false);
        HoleManager.Instance.SpawnHole();
    }

    public void Stun()
	{
        _animator.SetFloat("StunTime", 0.0F);
        _animator.SetOnlyTrigger("Stun");
	}

    private void ChangeFloors(bool isUp, bool isCheatButton)
	{
        FloorNumber += HitHead ? 0 : isUp ? 1 : -1;

        if(FloorNumber < 0)
        {
            if(isCheatButton)
			{
                FloorNumber = 7;
			}
            else
			{
                throw new System.Exception("This shouldn't happen. Figure it out"); //TODO: Remove this when you're done testing.
			}
        }
        else if(FloorNumber == 0 && !isCheatButton)
		{
            RemoveLife();
        }
        else if(FloorNumber > 7)
		{
            if(isCheatButton)
			{
                FloorNumber = 0;
			}
            else
			{
                Time.timeScale = 0.0F;
                //TODO: Win Level
			}
		}

        WarpManager.Instance.PlaceObjectOnFloor(LennyGameObject, FloorNumber, _floorOffset);
    }

    private void RemoveLife()
	{
        RemainingLives--;

        if(RemainingLives <= 0)
        {
            RemainingLives = 0;
            Time.timeScale = 0.0F;
            //TODO: GameOver
        }
    }

//Debugging methods
#if UNITY_EDITOR
    private void UpdateTestingShortcuts()
	{
        if(Input.GetKeyDown(KeyCode.Equals) || Input.GetKeyDown(KeyCode.KeypadPlus))
		{
            ChangeFloors(isUp: true, isCheatButton: true);
        }
        else if(Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            ChangeFloors(isUp: false, isCheatButton: true);
        }
        else if(Input.GetKeyDown(KeyCode.S))
		{
            _animator.SetOnlyTrigger("Stun");
		}
    }
#endif
}
