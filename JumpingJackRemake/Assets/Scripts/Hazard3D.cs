using UnityEngine;

public class Hazard3D : MonoBehaviour
{
	[SerializeField] private bool _isTransparent;
	private GameObject _parent;
	private float _rotationTotal;
	private SkinnedMeshRenderer _renderer;

	private void Start()
	{
		_renderer = GetComponentInChildren<SkinnedMeshRenderer>();
		_renderer.material = HazardManager3D.Instance.GetRandomMaterial(_isTransparent);
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

		if(transform.position.y < -50.0F)
		{
			WarpManager3D.Instance.PlaceObjectOnFloor(gameObject, floorNumber: 8, verticalOffset: 50.0F);
		}
    }
}
