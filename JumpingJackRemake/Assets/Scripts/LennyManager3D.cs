using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LennyManager3D : Manager<LennyManager3D>
{
    [SerializeField] private Lenny3D _lenny;
    [SerializeField] [Range(0.0F, 100.0F)] private float _runSpeed = 5.0F;

    private float _gravity;
    private float _positionalTheta = 0.0F;
    private bool _isInitiallyGrounded;

    public float GravityAcceleration => 30.0F;
    public float RunSpeed => _runSpeed;
    public Lenny3D Lenny => _lenny;
    public CharacterController CharacterController => _lenny.CharacterController;
    public Animator Animator { get; private set; }
    public bool IsJumping { get; set; }
    public bool JumpBeginVerticalAscent { get; set; }
    public bool JumpApex { get; set; }
    public bool JumpEndVerticalDescent { get; set; }

    private void Start()
    {
        Animator = GetComponentInChildren<Animator>();
        WarpManager3D.Instance.PlaceObjectOnFloor(_lenny.gameObject, 0, 1.0F);
        _positionalTheta = 0.0F;
        _isInitiallyGrounded = false;
        IsJumping = false;
    }

	private void Update()
	{
		if(!_isInitiallyGrounded)
		{
			CharacterController.Move(1.0F * Time.deltaTime * Vector3.down);
			_isInitiallyGrounded = CharacterController.isGrounded;
		}

		if(CharacterController.isGrounded)
		{
			_gravity = 1.0F;
		}
		else
		{
			_gravity += GravityAcceleration * Time.deltaTime;
		}

        if(!IsJumping)
		{
		    CharacterController.Move(_gravity * Time.deltaTime * Vector3.down);
		}
	}

	public void ApplyUserMovement()
	{
        float horizontalAxis = Input.GetAxis("Horizontal");
        Vector3 positionToCenter = Vector3.zero - Lenny.transform.position;
        Vector3 forwardTangent = Vector3.Cross(Vector3.up, positionToCenter);

        if(horizontalAxis < 0.0)
        {
            forwardTangent *= -1.0F;
        }

        Lenny.transform.LookAt(Lenny.transform.position + forwardTangent);
        _positionalTheta += horizontalAxis * RunSpeed * Time.deltaTime;
        _positionalTheta = Mathf.Repeat(_positionalTheta, 2.0F * Mathf.PI);
        float cos = FloorManager3D.Instance.FloorRadius * Mathf.Cos(_positionalTheta);
        float sin = FloorManager3D.Instance.FloorRadius * Mathf.Sin(_positionalTheta);
        Lenny.transform.position = new Vector3(cos, Lenny.transform.position.y, sin);
    }
}
