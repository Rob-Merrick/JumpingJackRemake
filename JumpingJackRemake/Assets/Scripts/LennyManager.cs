using UnityEngine;

public class LennyManager : MonoBehaviour
{
    [SerializeField] private GameObject _lenny;
    [SerializeField] [Range(0.1F, 10.0F)] private float _runSpeed = 1.0F;

    private void Update()
    {
        if(Input.GetKey(KeyCode.RightArrow))
		{
            _lenny.transform.Translate(Time.deltaTime * _runSpeed * Vector3.right);
		}
        else if(Input.GetKey(KeyCode.LeftArrow))
		{
            _lenny.transform.Translate(Time.deltaTime * _runSpeed * Vector3.left);
        }
    }
}
