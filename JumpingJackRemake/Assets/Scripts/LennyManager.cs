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
    private Vector3 _startLocation;

    public bool JumpInitialized { get; set; } = false;
    public bool JumpIsGood { get; set; } = false;
    public bool HitHead { get; set; } = false;
    public bool IsKonamiCodeEnabled { get; set; } = false;
    public int RemainingLives { get; private set; } = 6;
    public int ActiveHoles { get; private set; } = 0;
    public int FloorNumber { get; private set; } = 0;
    public int FloorOffset => _floorOffset;
    public float LennySpeed => GameSettingsManager.Instance.RunSpeed + _lennySpeedDifference;
    public float StunTime => _stunTime;
    public float FallSpeed => _fallSpeed;
    public float JumpSpeed => _jumpSpeed;
    public SpriteRenderer SpriteRenderer => _spriteRenderer;
    public Animator Animator => _animator;
    public GameObject LennyGameObject => _lenny;

	private void Start()
	{
        _spriteRenderer = _lenny.GetComponent<SpriteRenderer>();
		_animator = _lenny.GetComponent<Animator>();
        _startLocation = _lenny.transform.position;
	}

	private void Update()
    {
        UpdateTestingShortcuts();
    }

	public void Restart()
	{
        JumpInitialized = false;
        JumpIsGood = false;
        HitHead = false;
        ActiveHoles = 0;
        FloorNumber = 0;
        _lenny.transform.position = _startLocation;
        _lenny.GetComponent<Lenny>().Restart();
	}

    public void GainLife()
	{
        RemainingLives++;
	}

    public void ResetLives()
	{
        RemainingLives = 6;
	}

	public void AddActiveHole()
	{
        ActiveHoles++;
	}

    public void RemoveActiveHole()
	{
        ActiveHoles--;
        ActiveHoles = Mathf.Max(ActiveHoles, 0);
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

        if(GameManager.Instance.IsReady)
		{
            HoleManager.Instance.SpawnHole();
            ScoreManager.Instance.AddPoints(5);
		}
    }

    public void Stun()
	{
        ScreenManager.Instance.FlashScreenPink();
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
            GameManager.Instance.GameOver();
        }
    }

    //Debugging methods
    private void UpdateTestingShortcuts()
	{
        if(!IsKonamiCodeEnabled)
		{
            return;
		}

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
        else if(Input.GetKeyDown(KeyCode.W))
		{
            GameManager.Instance.WinLevel();
		}
        else if(Input.GetKeyDown(KeyCode.L))
		{
            RemoveLife();
		}
    }
}
