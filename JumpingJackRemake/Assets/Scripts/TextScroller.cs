using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TextScroller : MonoBehaviour
{
    [SerializeField] [Range(0.0F, 30.0F)] private float _scrollRate = 8.0F;
    [SerializeField] [Range(0.0F, 30.0F)] private float _startDelay = 1.0F;
    [SerializeField] [Range(0.0F, 30.0f)] private float _endDelay = 3.0F;
    [SerializeField] private UnityEvent _finishedCallback = null;

    private string _originalText;
    private float _currentStartDelay;
    private float _currentEndDelay;
    private float _characterTimer;
    private TextMeshProUGUI _text;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _originalText = _text.text;
        _text.text = string.Empty;
        _currentStartDelay = 0.0F;
        _characterTimer = 0.0F;
    }

    private void Update()
    {
        if(_currentEndDelay >= _endDelay && _finishedCallback != null)
		{
            _finishedCallback.Invoke();
            enabled = false;
		}
        else if(_currentStartDelay >= _startDelay)
		{
            int totalCharacters = Mathf.Min((int) (_characterTimer * _scrollRate), _originalText.Length);
            _text.text = _originalText.Substring(0, totalCharacters);
            _characterTimer += Time.deltaTime;

            if(totalCharacters == _originalText.Length)
			{
                _currentEndDelay += Time.deltaTime;
			}
		}
        else
		{
            _currentStartDelay += Time.deltaTime;
		}
    }
}
