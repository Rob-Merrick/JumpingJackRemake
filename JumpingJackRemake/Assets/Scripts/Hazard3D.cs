using UnityEngine;

public class Hazard3D : MonoBehaviour
{
	[SerializeField] private bool _isTransparent;

	private float _rotationTotal;
	private GameObject _parent;
	private SkinnedMeshRenderer _renderer;

	private void Start()
	{
		_renderer = GetComponentInChildren<SkinnedMeshRenderer>();
		_renderer.material = HazardManager3D.Instance.GetRandomMaterial(_isTransparent);
		_parent = new GameObject($"{gameObject.name}Container");
		_parent.transform.SetParent(HazardManager3D.Instance.gameObject.transform, worldPositionStays: false);
		_parent.transform.position = Vector3.zero;
		gameObject.transform.SetParent(_parent.transform, worldPositionStays: false);
		gameObject.transform.position = FloorManager3D.Instance.FloorRadius * Vector3.left;
		(int floorNumber, float floorRotation) = SpawnManager3D.Instance.PickRandomFloorAndRotation(WarpManager3D.Instance.GetNearestFloor(gameObject));
		WarpManager3D.Instance.PlaceObjectOnFloor(_parent, floorNumber);
		_rotationTotal = floorRotation * Mathf.Rad2Deg;
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
