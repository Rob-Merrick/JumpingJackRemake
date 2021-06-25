using UnityEngine;

public class Hazard3D : MonoBehaviour
{
	private GameObject _parent;
	private float _rotationTotal;

	private void Start()
	{
		float y = gameObject.transform.position.y;
		_parent = new GameObject($"{gameObject.name}Container");
		_parent.transform.SetParent(HazardManager3D.Instance.gameObject.transform, worldPositionStays: false);
		_parent.transform.position = Vector3.zero;
		gameObject.transform.SetParent(_parent.transform, worldPositionStays: false);
		gameObject.transform.position = FloorManager3D.Instance.FloorRadius * Vector3.left;
		_parent.transform.position = new Vector3(0.0F, y, 0.0F);
		_rotationTotal = Random.Range(0.0F, 360.0F);
	}

	private void Update()
    {
		_rotationTotal += HazardManager3D.Instance.RunSpeed * Time.deltaTime;
		_parent.transform.rotation = Quaternion.Euler(_rotationTotal * Vector3.up);
    }
}