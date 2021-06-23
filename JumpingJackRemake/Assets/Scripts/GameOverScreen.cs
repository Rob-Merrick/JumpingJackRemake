using System;
using TMPro;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
	[SerializeField] private GameObject _finalScoreContainer;
	[SerializeField] private GameObject _newHighScoreContainer;
	[SerializeField] private GameObject _instructionsContainer;
	[SerializeField] private TextMeshProUGUI _finalScoreText;

	public void RefreshText()
	{
		int hazardCount = HazardManager.Instance.Hazards.Length;
		_finalScoreContainer.SetActive(false);
		_newHighScoreContainer.SetActive(false);
		_instructionsContainer.SetActive(false);
		string newText = $"FINAL SCORE {ScoreManager.Instance.CurrentScore:00000}{Environment.NewLine}WITH {hazardCount}  HAZARD{(hazardCount != 1 ? "S" : string.Empty)}";
		_finalScoreText.text = newText;
		_finalScoreContainer.GetComponentInChildren<TextScroller>().UpdateText(newText);
	}

	public void Next()
	{
		if(!_finalScoreContainer.activeSelf)
		{
			_finalScoreContainer.SetActive(true);
		}
		else if(ScoreManager.Instance.IsNewHighScore && !_newHighScoreContainer.activeSelf)
		{
			_newHighScoreContainer.SetActive(true);
		}
		else if(!_instructionsContainer.activeSelf)
		{
			_instructionsContainer.SetActive(true);
		}
	}
}
