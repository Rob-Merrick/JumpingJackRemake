using UnityEngine;

public class WarpManager : MonoBehaviour
{
    private int _floor0Position = -87;
    private int _deltaFloorPosition = 24;

    public static WarpManager Instance { get; private set; }
    
    private void Awake()
    {
        if(Instance != null)
		{
            throw new System.Exception($"Attempting to overwrite the singleton instance for {name}");
		}

        Instance = this;
    }

    public void PlaceObjectOnFloor(GameObject gameObject, int floorNumber, int floorPositionOffset)
	{
        ValidateFloor(gameObject, floorNumber);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, _floor0Position + floorNumber * _deltaFloorPosition + floorPositionOffset, gameObject.transform.position.z);
	}

    private void ValidateFloor(GameObject gameObject, int floorNumber)
	{
        if(floorNumber < 1 || floorNumber > 7)
        {
            if(floorNumber == 0)
            {
                if(gameObject != LennyManager.Instance.Lenny)
                {
                    throw new System.Exception($"Cannot place an object onto floor {floorNumber}. Only Lenny can be placed on the area below the first floor");
                }
            }
            else
            {
                throw new System.Exception($"Cannot place an object onto floor {floorNumber}. Floors must be in the range of [1, 7]");
            }
        }
    }
}
