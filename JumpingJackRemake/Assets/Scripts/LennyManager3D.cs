using UnityEngine;

public class LennyManager3D : Manager<LennyManager3D>
{
    [SerializeField] private Lenny3D _lenny;
    [SerializeField] [Range(0.0F, 100.0F)] private float _runSpeed = 5.0F;

    private float _gravity;
    private float _positionalTheta = 0.0F;
    private bool _isLifeLost = false;

    public float GravityAcceleration => 30.0F;
    public float RunSpeed => _runSpeed;
    public Lenny3D Lenny => _lenny;
    public CharacterController CharacterController => _lenny.CharacterController;
    public Animator Animator { get; private set; }
    public bool IsJumping { get; set; }
    public bool JumpBeginVerticalAscent { get; set; }
    public bool JumpApex { get; set; }
    public bool JumpEndVerticalDescent { get; set; }
    public bool IsHeadCollided { get; set; }
    public bool IsSmashingHead { get; set; }
    public bool IsHit { get; set; }

    private void Start()
    {
        Animator = GetComponentInChildren<Animator>();
    }

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

        if(Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            CharacterController.enabled = false;
            WarpManager3D.Instance.PlaceObjectOnFloor(_lenny.gameObject, WarpManager3D.Instance.GetNearestFloor(_lenny.gameObject) + 1);
            CharacterController.enabled = true;
        }

        if(GameManager3D.Instance.IsReady)
		{
            if(_lenny.gameObject.transform.position.y < -50)
		    {
                _isLifeLost = true;
                GameManager3D.Instance.LoseLife();
		    }
            
            if(_lenny.gameObject.transform.position.y >= WarpManager3D.Instance.GetFloorHeight(7) - 1.0F && CharacterController.isGrounded)
			{
                GameManager3D.Instance.WinLevel();
                Animator.SetOnlyTrigger("Cheering");
			}

            if(Input.GetKeyDown(KeyCode.W))
			{
                GameManager3D.Instance.WinLevel();
                Animator.SetOnlyTrigger("Cheering");
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
        _lenny.transform.position = new Vector3(0.0F, 1.0F, 0.0F);
        _positionalTheta = SpawnManager3D.Instance.PickRandomFloorRotation(floorNumber: 0);
        ApplyUserMovement();

        if(CharacterController != null)
        {
            CharacterController.enabled = true;
        }
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
}
