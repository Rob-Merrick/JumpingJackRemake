using System.Collections.Generic;
using UnityEngine;

public class HazardManager3D : Manager<HazardManager3D>
{
    [SerializeField] private Hazard3D _carPrefab;
    [SerializeField] private Hazard3D _dinoPrefab;
    [SerializeField] private Hazard3D _ghostPrefab;
    [SerializeField] private Hazard3D _hatchetPrefab;
    [SerializeField] private Hazard3D _jetPrefab;
    [SerializeField] private Hazard3D _snakePrefab;
    [SerializeField] private float _runSpeed = 30.0F;

    private readonly List<Hazard3D> _hazards = new List<Hazard3D>();

    public float RunSpeed => _runSpeed;

	private void Start()
	{
        Hazard3D car = Instantiate(_carPrefab);
        Hazard3D dino = Instantiate(_dinoPrefab);
        Hazard3D ghost = Instantiate(_ghostPrefab);
        Hazard3D hatchet = Instantiate(_hatchetPrefab);
        Hazard3D jet = Instantiate(_jetPrefab);
        Hazard3D snake = Instantiate(_snakePrefab);
        _hazards.Add(car);
        _hazards.Add(dino);
        _hazards.Add(ghost);
        _hazards.Add(hatchet);
        _hazards.Add(jet);
        _hazards.Add(snake);

        for(int i = 0; i < _hazards.Count; i++)
		{
            WarpManager3D.Instance.PlaceObjectOnFloor(_hazards[i].gameObject, Random.Range(1, 8));
		}
    }
}
