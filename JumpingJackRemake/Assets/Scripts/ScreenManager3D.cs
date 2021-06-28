using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager3D : Manager<ScreenManager3D>
{
	[SerializeField] private Image _screenFadeImage;
	[SerializeField] private GameObject _countdownContainer;

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
