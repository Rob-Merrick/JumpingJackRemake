using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlternateColors : MonoBehaviour
{
    [SerializeField] [Range(0.1F, 10.0F)] private float _blinkRate = 0.6F;

    private Image _backgroundImage;
    private TextMeshProUGUI _text;
    private Color _primaryColor;
    private Color _secondaryColor;
    private float _totalTime;

    private void Start()
    {
        _backgroundImage = GetComponent<Image>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _primaryColor = _backgroundImage.color;
        _secondaryColor = _text.color;
    }

	private void OnEnable()
	{
		_totalTime = 0.0F;
	}

	private void Update()
    {
        _totalTime += Time.deltaTime;
        bool isSwapped = _totalTime % _blinkRate < _blinkRate / 2.0F;
        _backgroundImage.color = isSwapped ? _primaryColor : _secondaryColor;
        _text.color = isSwapped ? _secondaryColor : _primaryColor;
    }
}
