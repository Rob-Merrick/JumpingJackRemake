using UnityEngine;

public class LennyManager : Manager<LennyManager>
{
    [SerializeField] private GameObject _lenny;
    [SerializeField] private int _floorOffset = -1;
    [SerializeField] private float _lennySpeedDifference = 10.0F;
    [SerializeField] [Range(1.0F, 100.0F)] private float _fallSpeed = 50.0F;
    [SerializeField] [Range(1.0F, 100.0F)] private float _jumpSpeed = 75.0F;
    [SerializeField] [Range(0.0F, 5.0F)] private float _stunTime = 2.0F;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

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
        ScoreManager.Instance.AddPoints(5);
    }

    public void Stun()
	{
        _animator.SetFloat("StunTime", 0.0F);
        _animator.SetOnlyTrigger("Stun");
	}

    private void ChangeFloors(bool isUp, bool isCheatButton)
	{
        FloorNumber += HitHead ? 0 : isUp ? 1 : -1;

        if(FloorNumber < 0 && isCheatButton)
        {
            FloorNumber = 7;
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
                GameManager.Instance.WinLevel();
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
            GameManager.Instance.Restart();
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
