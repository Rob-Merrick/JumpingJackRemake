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

	private readonly Color[] _colorListLookup = new [] { Color.blue, Color.green, Color.magenta, Color.yellow, Color.black, Color.red };
	private readonly Hazard[] _hazardsLookup = new Hazard[10];

	private readonly List<Hazard> _hazards = new List<Hazard>();

	public Hazard[] Hazards => _hazards.ToArray();

	private void Start()
	{
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

		for(int i = 0; i < allHazards.Count; i++)
		{
			int randomIndex = Random.Range(0, allHazards.Count);
			_hazardsLookup[i] = allHazards[Random.Range(0, allHazards.Count)];
			allHazards.RemoveAt(randomIndex);
		}
	}

	private void Initialize()
	{
		for(int i = 0; i < GameManager.Instance.CurrentLevel; i++)
		{
			Hazard hazard = Instantiate(_hazardsLookup[i % _hazardsLookup.Length]);
			hazard.transform.SetParent(gameObject.transform, worldPositionStays: false);
			hazard.GetComponent<SpriteRenderer>().color = _colorListLookup[i % _colorListLookup.Length];
			_hazards.Add(hazard);
		}
	}
}
