using UnityEngine;

public class Hole3D : MonoBehaviour
{
    [SerializeField] private float _startRotationRadians = 0.0F;
    [SerializeField] private MoveAIDirection _moveDirection = MoveAIDirection.LeftUp;

    private float _size;
    private bool _isInitialized = false;
    
    public int FloorNumber { get; set; }
    public int HoleIndex { get; set; }
    public float CurrentRotation { get; private set; }
    public float StartRotationRadians { get => _startRotationRadians; set => _startRotationRadians = value; }
    public float Size => _size;
    public MoveAIDirection MoveDirection { get => _moveDirection; set => _moveDirection = value; }

    public void Initialize()
	{
        _size = HoleManager3D.Instance.HoleSizeRadians;
        CurrentRotation = _startRotationRadians;
        CurrentRotation = Mathf.Repeat(CurrentRotation, 2.0F * Mathf.PI);
        _isInitialized = true;
    }

	private void Update()
    {
        if(!_isInitialized)
		{
            throw new System.Exception($"Don't forget to call {nameof(Hole3D)}.{nameof(Initialize)}() for {name}");
		}

        CurrentRotation += _moveDirection == MoveAIDirection.LeftUp ? HoleManager3D.Instance.RotationalSpeedRadians * Time.deltaTime : -HoleManager3D.Instance.RotationalSpeedRadians * Time.deltaTime;
        CurrentRotation = Mathf.Repeat(CurrentRotation, 2.0F * Mathf.PI);
	}
}
