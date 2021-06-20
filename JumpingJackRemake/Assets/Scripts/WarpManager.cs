using System.Collections.Generic;
using UnityEngine;

public class WarpManager : MonoBehaviour
{
    private static IDictionary<int, float> _floorPositions = new Dictionary<int, float>()
	{
        { 0, -2.84F },
        { 1, -1.80F },
        { 2, -0.77F },
        { 3,  0.28F },
        { 4,  1.30F },
        { 5,  2.36F },
        { 6,  3.39F },
        { 7,  4.42F }
    };

    public static WarpManager Instance { get; private set; }
    
    private void Awake()
    {
        if(Instance != null)
		{
            throw new System.Exception($"Attempting to overwrite the singleton instance for {name}");
		}

        Instance = this;
    }

    public void PlaceObjectOnFloor(GameObject gameObject, int floorNumber, float floorPositionOffset)
	{
        ValidateFloor(gameObject, floorNumber);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, _floorPositions[floorNumber] + floorPositionOffset, gameObject.transform.position.z);
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
