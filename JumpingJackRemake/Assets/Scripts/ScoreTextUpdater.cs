using TMPro;
using UnityEngine;

public class ScoreTextUpdater : MonoBehaviour
{
    [SerializeField] private bool _isHighScore;

    private int? _previousScore;
    private TextMeshProUGUI _scoreText;

    private int Score => _isHighScore ? ScoreManager.Instance.HighScore : ScoreManager.Instance.CurrentCore;
    private string Prefix => _isHighScore ? "HI" : "SC";

	private void Start()
	{
		_scoreText = GetComponent<TextMeshProUGUI>();
	}

	private void Update()
    {
        if(_previousScore != Score)
        {
            _scoreText.text = $"{Prefix}{Score:00000}";
            _previousScore = Score;
        }
    }
}
