using System;
using UnityEngine;

public class WarpManager3D : Manager<WarpManager3D>
{
    private readonly float _floorDelta = 7.0F;
	private int FloorDeltaInt => Mathf.RoundToInt(_floorDelta);

	public float GetFloorHeight(int floorNumber, float verticalOffset = 0.0F)
	{
		return floorNumber * _floorDelta + verticalOffset;
	}

	public int GetNearestFloor(GameObject gameObject)
	{
		return Mathf.Clamp(Mathf.RoundToInt(gameObject.transform.position.y / _floorDelta), 0, 7);
	}

	public int GetNearestFloorBelow(GameObject gameObject)
	{
		return Mathf.Clamp(Mathf.FloorToInt(gameObject.transform.position.y / _floorDelta), 0, 7);
	}

	public int GetNearestFloorAbove(GameObject gameObject)
	{
		return Mathf.Clamp(Mathf.CeilToInt(gameObject.transform.position.y / _floorDelta), 0, 7);
	}

	public int GetNearestFloorHeight(GameObject gameObject)
	{
		return GetNearestFloor(gameObject) * FloorDeltaInt;
	}

	public int GetNearestFloorBelowHeight(GameObject gameObject)
	{
		return GetNearestFloorBelow(gameObject) * FloorDeltaInt;
	}

	public int GetNearestFloorAboveHeight(GameObject gameObject)
	{
		return GetNearestFloorAbove(gameObject) * FloorDeltaInt;
	}

	public void PlaceObjectOnFloor(GameObject gameObject, int floorNumber, float verticalOffset = 0.0F)
	{
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, GetFloorHeight(floorNumber, verticalOffset), gameObject.transform.position.z);
	}
}
