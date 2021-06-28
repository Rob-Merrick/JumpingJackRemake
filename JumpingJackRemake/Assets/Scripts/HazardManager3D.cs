using System.Collections.Generic;
using UnityEngine;

public class HazardManager3D : Manager<HazardManager3D>
{
    [SerializeField] private float _runSpeed = 30.0F;
    [SerializeField] private Hazard3D _carPrefab;
    [SerializeField] private Hazard3D _dinoPrefab;
    [SerializeField] private Hazard3D _ghostPrefab;
    [SerializeField] private Hazard3D _hatchetPrefab;
    [SerializeField] private Hazard3D _jetPrefab;
    [SerializeField] private Hazard3D _snakePrefab;
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

	private readonly Hazard3D[] _hazardPrefabs = new Hazard3D[6];
	private readonly List<Hazard3D> _hazards = new List<Hazard3D>();
	private bool _isAnimationStarted;

	public float RunSpeed => GameManager3D.Instance.IsReady ? _runSpeed : 0.0F;
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

	private void Start()
	{
		_hazardPrefabs[0] = _carPrefab;
		_hazardPrefabs[1] = _dinoPrefab;
		_hazardPrefabs[2] = _ghostPrefab;
		_hazardPrefabs[3] = _hatchetPrefab;
		_hazardPrefabs[4] = _jetPrefab;
		_hazardPrefabs[5] = _snakePrefab;
	}

	private void Update()
	{
		if(!_isAnimationStarted && GameManager3D.Instance.IsReady)
		{
			SetAnimationPlayingState(isPlaying: true);
			_isAnimationStarted = true;
		}
	}

	public void Restart()
	{
		_isAnimationStarted = false;
		
		for(int i = 0; i < _hazards.Count; i++)
		{
			Destroy(_hazards[i].gameObject);
		}

		_hazards.Clear();

		for(int i = 0; i < GameManager3D.Instance.Level - 1; i++)
		{
			Hazard3D hazard = Instantiate(_hazardPrefabs[i % _hazardPrefabs.Length]);
			_hazards.Add(hazard);
		}

		SetAnimationPlayingState(isPlaying: false);
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

	public void SetAnimationPlayingState(bool isPlaying)
	{
		float animationSpeed = isPlaying ? 1.0F : 0.0F;

		foreach(Hazard3D hazard in _hazards)
		{
			hazard.GetComponent<Animator>().speed = animationSpeed;
		}
	}
}
