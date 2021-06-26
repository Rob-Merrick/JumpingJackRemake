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
	[SerializeField] private Material _redColor;
	[SerializeField] private Material _pinkColor;
	[SerializeField] private Material _yellowColor;
	[SerializeField] private Material _greenColor;
	[SerializeField] private Material _blueColor;
	[SerializeField] private Material _blackColor;
	[SerializeField] private Material _redColorTransparent;
	[SerializeField] private Material _pinkColorTransparent;
	[SerializeField] private Material _yellowColorTransparent;
	[SerializeField] private Material _greenColorTransparent;
	[SerializeField] private Material _blueColorTransparent;
	[SerializeField] private Material _blackColorTransparent;

	public Material RedColor => _redColor;
	public Material PinkColor => _pinkColor;
	public Material YellowColor => _yellowColor;
	public Material GreenColor => _greenColor;
	public Material BlueColor => _blueColor;
	public Material BlackColor => _blackColor;
	public Material RedColorTransparent => _redColorTransparent;
	public Material PinkColorTransparent => _pinkColorTransparent;
	public Material YellowColorTransparent => _yellowColorTransparent;
	public Material GreenColorTransparent => _greenColorTransparent;
	public Material BlueColorTransparent => _blueColorTransparent;
	public Material BlackColorTransparent => _blackColorTransparent;

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

	public Material GetRandomMaterial(bool isTransparent)
	{
		int randomMaterialIndex = isTransparent ? Random.Range(6, 12) : Random.Range(0, 5);

		switch(randomMaterialIndex)
		{
			case  0: return _redColor;
			case  1: return _pinkColor;
			case  2: return _yellowColor;
			case  3: return _greenColor;
			case  4: return _blueColor;
			case  5: return _blackColor;
			case  6: return _redColorTransparent;
			case  7: return _pinkColorTransparent;
			case  8: return _yellowColorTransparent;
			case  9: return _greenColorTransparent;
			case 10: return _blueColorTransparent;
			case 11: return _blackColorTransparent;
			default: throw new System.NotImplementedException();
		}
	}
}
