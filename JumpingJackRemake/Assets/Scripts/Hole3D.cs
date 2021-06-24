using UnityEngine;

public class Hole3D : MonoBehaviour
{
    [SerializeField] private float _startRotationRadians = 0.0F;
    [SerializeField] private MoveAIDirection _moveAIDirection = MoveAIDirection.LeftUp;
    
    public float CurrentRotation { get; private set; }
    public int FloorNumber { get; private set; } = 1;

	private void Start()
	{
		CurrentRotation = _startRotationRadians;
	}

	private void Update()
    {
        CurrentRotation += _moveAIDirection == MoveAIDirection.LeftUp ? HoleManager3D.Instance.RotationalSpeedRadians * Time.deltaTime : -HoleManager3D.Instance.RotationalSpeedRadians * Time.deltaTime;
        CurrentRotation = Mathf.Repeat(CurrentRotation, 2.0F * Mathf.PI);
    }
}
