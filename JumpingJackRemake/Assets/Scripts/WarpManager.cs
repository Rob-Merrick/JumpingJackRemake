using UnityEngine;

public class WarpManager : Manager<WarpManager>
{
    private readonly int _floor0Position = -87;
    private readonly int _deltaFloorPosition = 24;
    private readonly int _deltaCeilingOffset = -18;

    public void PlaceObjectOnCeiling(GameObject gameObject, int floorNumberBelow)
	{
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, _floor0Position + (floorNumberBelow + 1) * _deltaFloorPosition + _deltaCeilingOffset, gameObject.transform.position.z);
    }

    public void PlaceObjectOnFloor(GameObject gameObject, int floorNumber, int floorPositionOffset)
	{
#if UNITY_EDITOR
        ValidateFloor(gameObject, floorNumber);
#else
        floorNumber = Mathf.Clamp(floorNumber, 0, 8);
#endif
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, _floor0Position + floorNumber * _deltaFloorPosition + floorPositionOffset, gameObject.transform.position.z);
	}

#if UNITY_EDITOR
	private void ValidateFloor(GameObject gameObject, int floorNumber)
	{
		if(floorNumber < 1 || floorNumber > 7)
		{
			if(floorNumber == 0)
			{
				if(gameObject != LennyManager.Instance.LennyGameObject && gameObject.name != "LennyFallTarget")
				{
					throw new System.Exception($"Cannot place an object onto floor {floorNumber}. Only Lenny can be placed on the area below the first floor");
				}
			}
			else if(floorNumber == 8)
			{
				if(gameObject.GetComponent<Hole>() == null && gameObject != LennyManager.Instance.LennyGameObject && gameObject.name != "LennyJumpTarget")
				{
					throw new System.Exception($"Cannot place an object onto floor {floorNumber}. Only holes can be placed on the top floor");
				}
			}
			else
			{
				throw new System.Exception($"Cannot place an object onto floor {floorNumber}. Floors must be in the range of [1, 7]");
			}
		}
	}
#endif
}
