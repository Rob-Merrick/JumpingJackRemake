using UnityEngine;

public class Hole3D : MonoBehaviour
{
    [SerializeField] [Range(0.0F, 2.0F * Mathf.PI)] private float _sizeRadians = 0.35F;
    [SerializeField] [Range(0.0F, 2.0F * Mathf.PI)] private float _rotationalSpeedRadians = 2.0F * Mathf.PI / 3.0F;

    public float Size => _sizeRadians;
    public float CurrentRotation { get; private set; } = 0.0F;

    private void Update()
    {
        CurrentRotation += _rotationalSpeedRadians * Time.deltaTime;
        CurrentRotation = Mathf.Repeat(CurrentRotation, 2.0F * Mathf.PI);
    }
}
