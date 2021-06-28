using UnityEngine;

public class LennyManager3D : Manager<LennyManager3D>
{
    [SerializeField] private Lenny3D _lenny;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] [Range(0.0F, 100.0F)] private float _runSpeed = 5.0F;

    private float _gravity;
    private float _positionalTheta = 0.0F;
    private bool _isLifeLost = false;

    public float GravityAcceleration => 30.0F;
    public float RunSpeed => _runSpeed;
    public Lenny3D Lenny => _lenny;
    public Animator Animator => _animator;
    public CharacterController CharacterController => _lenny.CharacterController;
    public bool IsJumping { get; set; }
    public bool JumpBeginVerticalAscent { get; set; }
    public bool JumpApex { get; set; }
    public bool JumpEndVerticalDescent { get; set; }
    public bool IsHeadCollided { get; set; }
    public bool IsSmashingHead { get; set; }
    public bool IsHit { get; set; }
    public int Lives { get; private set; } = 6;

	private void Update()
	{
		if(CharacterController.isGrounded)
		{
			_gravity = 1.0F;
		}
		else
		{
			_gravity += GravityAcceleration * Time.deltaTime;
		}

        if(!IsJumping && !IsSmashingHead)
		{
		    CharacterController.Move(_gravity * Time.deltaTime * Vector3.down);
		}

        if(GameManager3D.Instance.IsReady)
		{
            UpdateDebuggingMethods();

            if(_lenny.gameObject.transform.position.y < -50)
		    {
                LoseLife();
		    }
            
            if(_lenny.gameObject.transform.position.y >= WarpManager3D.Instance.GetFloorHeight(7) - 1.0F && CharacterController.isGrounded)
			{
                WinLevel();
			}
		}
	}

    public void Restart()
	{
        if(CharacterController != null)
		{
            CharacterController.enabled = false;
		}

        IsJumping = false;
        JumpBeginVerticalAscent = false;
        JumpApex = false;
        JumpEndVerticalDescent = false;
        IsHeadCollided = false;
        IsSmashingHead = false;
        IsHit = false;
        Animator.SetOnlyTrigger(_isLifeLost ? "LoseLevelRestart" : "Idle");
        _gravity = 0.0F;
        _isLifeLost = false;
        _lenny.transform.position = new Vector3(0.0F, 2.0F, 0.0F);
        _positionalTheta = SpawnManager3D.Instance.PickRandomFloorRotation(floorNumber: 0);
        ApplyUserMovement();

        if(CharacterController != null)
        {
            CharacterController.enabled = true;
        }
    }

    public void DecreaseLife()
	{
        Lives--;

        if(Lives <= 0)
		{
            Lives = 0;
		}
	}

    public void ResetLives()
	{
        Lives = 6;
	}

	public void ApplyUserMovement()
	{
        float horizontalAxis = GameManager3D.Instance.IsReady ? Input.GetAxis("Horizontal") : 0.0F;
        Vector3 positionToCenter = Vector3.zero - _lenny.transform.position;
        Vector3 forwardTangent = Vector3.Cross(Vector3.up, positionToCenter);

        if(horizontalAxis < 0.0)
        {
            forwardTangent *= -1.0F;
        }

        _lenny.transform.LookAt(_lenny.transform.position + forwardTangent);
        _positionalTheta += horizontalAxis * RunSpeed * Time.deltaTime;
        _positionalTheta = Mathf.Repeat(_positionalTheta, 2.0F * Mathf.PI);
        float cos = FloorManager3D.Instance.FloorRadius * Mathf.Cos(_positionalTheta);
        float sin = FloorManager3D.Instance.FloorRadius * Mathf.Sin(_positionalTheta);
        _lenny.transform.position = new Vector3(cos, _lenny.transform.position.y, sin);
    }

    private void LoseLife()
	{
        _isLifeLost = true;
        SoundManager3D.Instance.PlaySoundWithAudioSource($"Scream{Random.Range(1, 5)}", _audioSource);

        if(Lives - 1 <= 0)
		{
            GameManager3D.Instance.LoseGame();
		}
        else
		{
            GameManager3D.Instance.LoseLife();
		}
    }

    private void WinLevel()
	{
        this.DoAfter(seconds: 1.0F, () => SoundManager3D.Instance.PlaySound("Hooray"));
        GameManager3D.Instance.WinLevel();
        this.DoAfter(seconds: 1.0F, () => Animator.SetOnlyTrigger("Cheering"));
    }

    //Debugging Methods
    private void UpdateDebuggingMethods()
	{
  //      if(KonamiCodeChecker.Instance == null || !KonamiCodeChecker.IsKonamiCodeEnabled)
		//{
  //          return;
		//}

        if(Input.GetKeyDown(KeyCode.L))
		{
            WarpLenny(new Vector3(_lenny.transform.position.x, -8.0F, _lenny.transform.position.z));
		}

        if(Input.GetKeyDown(KeyCode.G))
		{
            Lives = 1;
            WarpLenny(new Vector3(_lenny.transform.position.x, -8.0F, _lenny.transform.position.z));
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            WinLevel();
        }

        if(Input.GetKeyDown(KeyCode.Equals) || Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            WarpFloors(isUp: true);
        }

        if(Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            WarpFloors(isUp: false);
        }
    }

    private void WarpLenny(Vector3 newPosition)
	{
        CharacterController.enabled = false;
        _lenny.gameObject.transform.position = newPosition;
        CharacterController.enabled = true;
    }

    private void WarpFloors(bool isUp)
	{
        CharacterController.enabled = false;
        WarpManager3D.Instance.PlaceObjectOnFloor(_lenny.gameObject, WarpManager3D.Instance.GetNearestFloor(_lenny.gameObject) + (isUp ? 1 : -1));
        CharacterController.enabled = true;
    }
}
