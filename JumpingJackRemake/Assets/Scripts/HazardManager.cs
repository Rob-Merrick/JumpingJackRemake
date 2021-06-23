using System.Collections.Generic;
using UnityEngine;

public class HazardManager : Manager<HazardManager>
{
    [SerializeField] private Hazard _carPrefab;
	[SerializeField] private Hazard _dinoPrefab;
	[SerializeField] private Hazard _ghostPrefab;
	[SerializeField] private Hazard _hatchetPrefab;
	[SerializeField] private Hazard _jetPrefab;
	[SerializeField] private Hazard _shotgunPrefab;
	[SerializeField] private Hazard _snakePrefab;
	[SerializeField] private Hazard _squidPrefab;
	[SerializeField] private Hazard _trainPrefab;
	[SerializeField] private Hazard _witchPrefab;
	[SerializeField] private Color _blue;
	[SerializeField] private Color _green;
	[SerializeField] private Color _magenta;
	[SerializeField] private Color _yellow;
	[SerializeField] private Color _black;
	[SerializeField] private Color _red;

	private Color[] _colorListLookup;
	private readonly Hazard[] _hazardsLookup = new Hazard[10];

	private readonly List<Hazard> _hazards = new List<Hazard>();

	public Hazard[] Hazards => _hazards.ToArray();

	private void Start()
	{
		_colorListLookup = new[] { _blue, _green, _magenta, _yellow, _black, _red };
		GenerateRandomHazardOrder();
	}

	public void Restart()
	{
		foreach(Hazard hazard in Hazards)
		{
			Destroy(hazard.gameObject);
		}

		_hazards.Clear();
		Initialize();
	}

	private void GenerateRandomHazardOrder()
	{
		List<Hazard> allHazards = new List<Hazard>();
		allHazards.Add(_carPrefab);
		allHazards.Add(_dinoPrefab);
		allHazards.Add(_ghostPrefab);
		allHazards.Add(_hatchetPrefab);
		allHazards.Add(_jetPrefab);
		allHazards.Add(_shotgunPrefab);
		allHazards.Add(_snakePrefab);
		allHazards.Add(_squidPrefab);
		allHazards.Add(_trainPrefab);
		allHazards.Add(_witchPrefab);

		for(int i = 0; i < _hazardsLookup.Length; i++)
		{
			int randomIndex = Random.Range(0, allHazards.Count);
			_hazardsLookup[i] = allHazards[randomIndex];
			allHazards.RemoveAt(randomIndex);
		}
	}

	private void Initialize()
	{
		for(int i = 0; i < GameManager.Instance.CurrentLevel && i < 20; i++)
		{
			Hazard hazard = Instantiate(_hazardsLookup[i % _hazardsLookup.Length]);
			hazard.transform.SetParent(gameObject.transform, worldPositionStays: false);
			hazard.GetComponent<SpriteRenderer>().color = _colorListLookup[i % _colorListLookup.Length];
			_hazards.Add(hazard);
		}
	}
}
