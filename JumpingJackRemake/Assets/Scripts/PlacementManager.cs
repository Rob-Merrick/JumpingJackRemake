using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : Manager<PlacementManager>
{
	private readonly List<(int floorNumber, Vector2Int screenPosition)> _randomPositions = new List<(int floorNumber, Vector2Int screenPosition)>();

	public void Restart()
	{
		_randomPositions.Clear();
		GameObject floorPositionTest = new GameObject();

		for(int floorNumber = 1; floorNumber <= 7; floorNumber++)
		{
			for(float horizontalDivision = -ScreenManager.Instance.PlayableAreaRightEdge; horizontalDivision < ScreenManager.Instance.PlayableAreaRightEdge; horizontalDivision += ScreenManager.Instance.PlayableAreaHorizontalDistance / 8.0F)
			{
				float horizontalVariance = Random.Range(-8.0F, 8.0F); //This should leave enough of a gap between each hazard for Lenny, who has a 16x18 pixel collider box
				floorPositionTest.transform.position = new Vector3(horizontalDivision + horizontalVariance, 0.0F, 0.0F);
				WarpManager.Instance.PlaceObjectOnFloor(floorPositionTest, floorNumber, 0);
				_randomPositions.Add((floorNumber, new Vector2Int((int) floorPositionTest.transform.position.x, (int) floorPositionTest.transform.position.y)));
			}
		}

		Destroy(floorPositionTest);
	}

	public (int floorNumber, Vector2Int screenPosition) GetRandomPosition()
	{
		if(_randomPositions.Count == 0)
		{
			throw new System.Exception("Cannot get a random position because the preset list is empty. Make sure to call Restart() between each level");
		}

		int randomIndex = Random.Range(0, _randomPositions.Count);
		(int, Vector2Int) result = _randomPositions[randomIndex];
		_randomPositions.RemoveAt(randomIndex);
		return result;
	}
}
