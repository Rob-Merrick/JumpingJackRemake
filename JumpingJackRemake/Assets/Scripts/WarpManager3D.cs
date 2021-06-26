using UnityEngine;

public class WarpManager3D : Manager<WarpManager3D>
{
    private readonly float _floorDelta = 7.0F;

    public void PlaceObjectOnFloor(GameObject gameObject, int floorNumber)
	{
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, floorNumber * _floorDelta, gameObject.transform.position.z);
	}
}
