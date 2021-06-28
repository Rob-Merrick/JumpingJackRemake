using System.Collections.Generic;
using UnityEngine;

public class SpawnManager3D : Manager<SpawnManager3D>
{
	private readonly IDictionary<int, List<(Vector3 position, float rotation)>> _floorRotationalPositionLookup = new Dictionary<int, List<(Vector3 position, float rotation)>>();
	private const float _floorRotationDivisions = 10.0F;
	private const float _floorRotationDelta = 2.0F * Mathf.PI / _floorRotationDivisions;

	public void Restart()
	{
		RebuildRandomPositions();
	}

	public (int floorNumber, Vector3 floorPosition) PickRandomFloorAndPosition(float verticalOffset = 0.0F)
	{
		(int floorNumber, (Vector3 floorPosition, float floorRotation) spot) = PickRandomFloorAndSpot(verticalOffset);
		return (floorNumber, spot.floorPosition);
	}

	public (int floorNumber, float floorPosition) PickRandomFloorAndRotation(float verticalOffset = 0.0F)
	{
		(int floorNumber, (Vector3 floorPosition, float floorRotation) spot) = PickRandomFloorAndSpot(verticalOffset);
		return (floorNumber, spot.floorRotation);
	}

	public Vector3 PickRandomFloorPosition(int floorNumber, float verticalOffset = 0.0F)
	{
		return PickRandomFloorSpot(floorNumber, verticalOffset).floorPosition;
	}

	public float PickRandomFloorRotation(int floorNumber)
	{
		return PickRandomFloorSpot(floorNumber, 0.0F).floorRotation;
	}

	private (Vector3 floorPosition, float floorRotation) PickRandomFloorSpot(int floorNumber, float verticalOffset)
	{
		if(TryPickRandomFloorSpot(floorNumber, verticalOffset, out (Vector3, float)? spot))
		{
			return spot.Value;
		}

		throw new System.Exception($"Cannot get a random spot on floor {floorNumber} because there are no available spots left for that floor. Make sure to call Restart() between levels");
	}

	private (int floorNumber, (Vector3 floorPosition, float floorRotation)) PickRandomFloorAndSpot(float verticalOffset = 0.0F)
	{
		List<int> availableFloors = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7 };

		while(availableFloors.Count > 0)
		{
			int randomFloorIndex = Random.Range(0, availableFloors.Count);
			int randomFloor = availableFloors[randomFloorIndex];
			availableFloors.RemoveAt(randomFloorIndex);

			if(TryPickRandomFloorSpot(randomFloor, verticalOffset, out (Vector3 floorPosition, float floorRotation)? randomFloorSpot))
			{
				return (randomFloor, randomFloorSpot.Value);
			}
		}

		throw new System.Exception($"Cannot get a random floor and position because there are no available positions left. Make sure to call Restart() between levels");
	}

	private bool TryPickRandomFloorSpot(int floorNumber, float verticalOffset, out (Vector3 position, float rotation)? result)
	{
		bool isSuccessful = false;
		result = null;
		List<(Vector3 position, float rotation)> availableFloorPositions = _floorRotationalPositionLookup[floorNumber];

		if(availableFloorPositions.Count > 0)
		{
			int randomIndex = Random.Range(0, availableFloorPositions.Count);
			result = availableFloorPositions[randomIndex];
			availableFloorPositions.RemoveAt(randomIndex);
			Vector3 newPosition = new Vector3(result.Value.position.x, result.Value.position.y + verticalOffset, result.Value.position.z);
			result = (newPosition, result.Value.rotation);
			isSuccessful = true;
		}

		return isSuccessful;
	}

	private void RebuildRandomPositions()
	{
		_floorRotationalPositionLookup.Clear();

		for(int floorNumber = 0; floorNumber < 8; floorNumber++)
		{
			_floorRotationalPositionLookup.Add(floorNumber, new List<(Vector3 position, float rotation)>());
			float floorHeight = WarpManager3D.Instance.GetFloorHeight(floorNumber);

			for(float rotationSubdivision = 0.0F; rotationSubdivision < _floorRotationDivisions; rotationSubdivision += _floorRotationDelta)
			{
				Vector3 floorPosition = new Vector3(FloorManager3D.Instance.FloorRadius * Mathf.Cos(rotationSubdivision), floorHeight, FloorManager3D.Instance.FloorRadius * Mathf.Sin(rotationSubdivision));
				_floorRotationalPositionLookup[floorNumber].Add((floorPosition, rotationSubdivision));
			}
		}
	}
}
