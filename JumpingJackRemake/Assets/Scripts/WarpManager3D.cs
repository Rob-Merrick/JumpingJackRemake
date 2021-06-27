using UnityEngine;

public class WarpManager3D : Manager<WarpManager3D>
{
    private readonly float _floorDelta = 7.0F;

	private int FloorDeltaInt => Mathf.RoundToInt(_floorDelta);

    public int GetNearestFloor(GameObject gameObject)
	{
		return Mathf.Clamp(Mathf.RoundToInt(gameObject.transform.position.y / _floorDelta) * FloorDeltaInt, 0, 8);
	}

	public int GetNearestFloorBelow(GameObject gameObject)
	{
		return Mathf.Clamp(Mathf.FloorToInt(gameObject.transform.position.y / _floorDelta), 0, 8);
	}

	public int GetNearestFloorAbove(GameObject gameObject)
	{
		return Mathf.Clamp(Mathf.CeilToInt(gameObject.transform.position.y / _floorDelta), 0, 8);
	}

	public void PlaceObjectOnFloor(GameObject gameObject, int floorNumber, float verticalOffset = 0.0F)
	{
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, floorNumber * _floorDelta + verticalOffset, gameObject.transform.position.z);
	}
}
