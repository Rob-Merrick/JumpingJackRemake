using UnityEngine;
using UnityEngine.Events;

public class DelayedCallback : MonoBehaviour
{
    [SerializeField] private UnityEvent _callback;
	[SerializeField] [Range(0.0F, 30.0F)] private float _delayTime = 5.0F;
	[SerializeField] private KeyCode _skipKey = KeyCode.Return;

	private float _totalTime;

	private void Start()
	{
		if(_callback == null)
		{
			throw new System.Exception("Missing the callback for this Delayed Callback script");
		}

		_totalTime = 0.0F;
	}

	private void Update()
    {
		_totalTime += Time.deltaTime;

		if(Input.GetKeyDown(_skipKey) || _totalTime >= _delayTime)
		{
			_callback.Invoke();
			enabled = false;
		}
    }
}
