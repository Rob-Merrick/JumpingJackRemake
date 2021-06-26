using UnityEngine;

public class Hole3D : MonoBehaviour
{
    [SerializeField] private float _startRotationRadians = 0.0F;
    [SerializeField] private MoveAIDirection _moveDirection = MoveAIDirection.LeftUp;
    
    public int FloorNumber { get; set; }
    public float CurrentRotation { get; private set; }
    public float StartRotationRadians { get => _startRotationRadians; set => _startRotationRadians = value; }
    public MoveAIDirection MoveDirection { get => _moveDirection; set => _moveDirection = value; }

	private void Start()
	{
		CurrentRotation = _startRotationRadians;
        CurrentRotation = Mathf.Repeat(CurrentRotation, 2.0F * Mathf.PI);
    }

	private void Update()
    {
        CurrentRotation += _moveDirection == MoveAIDirection.LeftUp ? HoleManager3D.Instance.RotationalSpeedRadians * Time.deltaTime : -HoleManager3D.Instance.RotationalSpeedRadians * Time.deltaTime;
        CurrentRotation = Mathf.Repeat(CurrentRotation, 2.0F * Mathf.PI);
    }
}
