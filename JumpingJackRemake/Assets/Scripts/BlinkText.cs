using TMPro;
using UnityEngine;

public class BlinkText : MonoBehaviour
{
	[SerializeField] [Range(0.1F, 10.0F)] private float _blinkRate = 2.0F;

    private TextMeshProUGUI _text;

	private void Start()
	{
		_text = GetComponent<TextMeshProUGUI>();
	}

	private void Update()
    {
        _text.enabled = Time.realtimeSinceStartup % _blinkRate < _blinkRate / 2.0F;
    }
}
