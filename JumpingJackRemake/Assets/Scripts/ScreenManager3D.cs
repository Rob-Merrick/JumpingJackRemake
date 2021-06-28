using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager3D : Manager<ScreenManager3D>
{
	[SerializeField] private Image _screenFadeImage;
	[SerializeField] private GameObject _countdownContainer;
	[SerializeField] private TextMeshProUGUI _gameOverText;
	[SerializeField] private LoseLifeScreen3D _lostLifeScreen;
	[SerializeField] private GainLevelScreen3D _gainLevelScreen;

	public void DisplayGameOver(int lastLevel, int highestLevel, Action callback = null)
	{
		_gameOverText.gameObject.SetActive(true);
		_gameOverText.text = $"GAME OVER{Environment.NewLine}{Environment.NewLine}LAST LEVEL - {lastLevel}{Environment.NewLine}HIGH LEVEL - {highestLevel}";
		_gameOverText.color = new Color(_gameOverText.color.r, _gameOverText.color.g, _gameOverText.color.b, 1.0F);
		this.DoAfter(seconds: 5.0F, () => StartCoroutine(FadeGameOverScreen(callback)));
	}

	public void ShowLostLife()
	{
		_lostLifeScreen.Display();
	}

	public void ShowGainedLevel()
	{
		_gainLevelScreen.Display();
	}

	public void StartCountdown(Action callback)
	{
		StartCoroutine(StartCountdownCoroutine(callback));
	}

	public void FadeToColor(Color color, float timeToFade = 2.0F, Action callback = null)
	{
		StartCoroutine(FadeToColorCoroutine(color, timeToFade, callback));
	}

	public void FadeFromColor(Color color, float timeToFade = 2.0F, Action callback = null)
	{
		StartCoroutine(FadeFromColorCoroutine(color, timeToFade, callback));
	}

	private IEnumerator FadeGameOverScreen(Action callback)
	{
		_gameOverText.color = new Color(_gameOverText.color.r, _gameOverText.color.g, _gameOverText.color.b, 1.0F);
		float timeTaken = 0.0F;

		while(Application.isPlaying && timeTaken < 10.0F)
		{
			_gameOverText.color = new Color(_gameOverText.color.r, _gameOverText.color.g, _gameOverText.color.b, Mathf.Lerp(1.0F, 0.0F, timeTaken / 10.0F));
			timeTaken += Time.deltaTime;
			yield return null;
		}

		_gameOverText.color = new Color(_gameOverText.color.r, _gameOverText.color.g, _gameOverText.color.b, 0.0F);
		_gameOverText.gameObject.SetActive(false);

		this.DoAfter(seconds: 5.0F, () => callback?.Invoke());
	}

	private IEnumerator StartCountdownCoroutine(Action callback)
	{
		_countdownContainer.SetActive(true);
		yield return new WaitForSeconds(3.0F);

		this.DoAfter(seconds: 2.0F, () =>
		{
			for(int i = 0; i < _countdownContainer.transform.childCount; i++)
			{
				_countdownContainer.transform.GetChild(i).gameObject.SetActive(false);
			}

			_countdownContainer.SetActive(false);
		});

		callback?.Invoke();
	}

	private IEnumerator FadeToColorCoroutine(Color color, float timeToFade, Action callback)
	{
		_screenFadeImage.enabled = true;
		_screenFadeImage.color = new Color(color.r, color.g, color.b, 0.0F);
		float timeTaken = 0.0F;

		while(Application.isPlaying && timeTaken < timeToFade)
		{
			_screenFadeImage.color = new Color(color.r, color.g, color.b, Mathf.Lerp(0.0F, 1.0F, timeTaken / timeToFade));
			timeTaken += Time.deltaTime;
			yield return null;
		}

		_screenFadeImage.color = new Color(color.r, color.g, color.b, 1.0F);
		callback?.Invoke();
	}

	private IEnumerator FadeFromColorCoroutine(Color color, float timeToFade = 2.0F, Action callback = null)
	{
		_screenFadeImage.enabled = true;
		_screenFadeImage.color = new Color(color.r, color.g, color.b, 1.0F);
		float timeTaken = 0.0F;

		while(Application.isPlaying && timeTaken < timeToFade)
		{
			_screenFadeImage.color = new Color(color.r, color.g, color.b, Mathf.Lerp(1.0F, 0.0F, timeTaken / timeToFade));
			timeTaken += Time.deltaTime;
			yield return null;
		}

		_screenFadeImage.color = new Color(color.r, color.g, color.b, 0.0F);
		_screenFadeImage.enabled = false;
		callback?.Invoke();
	}
}
