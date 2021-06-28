using UnityEngine;

public class CameraRotateMenu : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 1.0F;

    private void Update()
    {
        transform.Rotate(_rotationSpeed * Time.deltaTime * Vector3.up);
    }
}
