using UnityEngine;

public class WarpManager3D : Manager<WarpManager3D>
{
    private readonly float _floorDelta = 7.0F;

    public void PlaceObjectOnFloor(GameObject gameObject, int floorNumber, float verticalOffset = 0.0F)
	{
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, floorNumber * _floorDelta + verticalOffset, gameObject.transform.position.z);
	}
}
