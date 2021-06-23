using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TextScroller : MonoBehaviour
{
    [SerializeField] [Range(0.0F, 30.0F)] private float _scrollRate = 8.0F;
    [SerializeField] [Range(0.0F, 30.0F)] private float _startDelay = 1.0F;
    [SerializeField] [Range(0.01F, 30.0f)] private float _endDelay = 3.0F;
    [SerializeField] private KeyCode _skipKey = KeyCode.Return;
    [SerializeField] private UnityEvent _finishedCallback = null;

    private bool _isFinishedScrolling;
    private string _originalText;
    private float _currentStartDelay;
    private float _currentEndDelay;
    private float _characterTimer;
    private TextMeshProUGUI _text;

	private void Awake()
	{
        _text = GetComponent<TextMeshProUGUI>();
        _originalText = _text.text;
    }

	private void OnEnable()
    {
        _isFinishedScrolling = false;
        _text.text = string.Empty;
        _currentStartDelay = 0.0F;
        _currentEndDelay = 0.0F;
        _characterTimer = 0.0F;
    }

	private void Update()
    {
        bool finishedCallbackExists = FinishedCallbackExists();

        if(Input.GetKeyDown(_skipKey) || (finishedCallbackExists && _currentEndDelay >= _endDelay) || (!finishedCallbackExists && _isFinishedScrolling))
		{
            _text.text = _originalText;
            _finishedCallback?.Invoke();
            enabled = false;
		}
        else if(_currentStartDelay >= _startDelay)
		{
            int totalCharacters = Mathf.Min((int) (_characterTimer * _scrollRate), _originalText.Length);
            _text.text = _originalText.Substring(0, totalCharacters);
            _characterTimer += Time.deltaTime;

            if(totalCharacters == _originalText.Length)
			{
                _isFinishedScrolling = true;
                _currentEndDelay += Time.deltaTime;
			}
		}
        else
		{
            _currentStartDelay += Time.deltaTime;
		}
    }

    public void UpdateText(string newText)
	{
        _originalText = newText;
	}

    private bool FinishedCallbackExists()
	{
        if(_finishedCallback == null)
		{
            return false;
		}

        for(int i = 0; i < _finishedCallback.GetPersistentEventCount(); i++)
		{
            if(_finishedCallback.GetPersistentTarget(i) != null && !string.IsNullOrWhiteSpace(_finishedCallback.GetPersistentMethodName(i)))
			{
                return true;
			}
		}

        return false;
	}
}
