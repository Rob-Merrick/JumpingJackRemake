using TMPro;
using UnityEngine;

public class LivesTextUpdate : MonoBehaviour
{
    private TextMeshProUGUI _livesText;
	private int? _previousLives = null;

	private void Start()
	{
		_livesText = GetComponent<TextMeshProUGUI>();
	}

	private void Update()
    {
        if(_previousLives != LennyManager.Instance.RemainingLives)
		{
			_livesText.text = LennyManager.Instance.RemainingLives > 0 ? new string('#', LennyManager.Instance.RemainingLives) : string.Empty;
			_previousLives = LennyManager.Instance.RemainingLives;
		}
    }
}
