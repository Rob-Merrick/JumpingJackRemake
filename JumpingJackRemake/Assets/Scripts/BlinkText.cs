using TMPro;
using UnityEngine;

public class BlinkText : MonoBehaviour
{
	[SerializeField] [Range(0.1F, 10.0F)] private float _blinkRate = 2.0F;

    private TextMeshProUGUI _text;
	private float _totalTime;

	private void Start()
	{
		_text = GetComponent<TextMeshProUGUI>();
	}

	private void OnEnable()
	{
		_totalTime = 0.0F;
	}

	private void Update()
    {
		_totalTime += Time.deltaTime;
        _text.enabled = _totalTime % _blinkRate < _blinkRate / 2.0F;
    }
}
