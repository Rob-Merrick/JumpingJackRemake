using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
	[SerializeField] private Image _blackOverlay;

	private void Update()
	{
		KonamiCodeChecker.Instance.CheckForKonamiCode();
	}

	public void StartGame()
	{
		StartCoroutine(FadeDownForSceneChange("ReimaginedMode"));
	}

	public void MainMenu()
	{
		StartCoroutine(FadeDownForSceneChange("Menu"));
	}

	private IEnumerator FadeDownForSceneChange(string newScene)
	{
		_blackOverlay.color = new Color(_blackOverlay.color.r, _blackOverlay.color.g, _blackOverlay.color.b, 0.0F);
		float timeTaken = 0.0F;

		while(Application.isPlaying && timeTaken < 2.0F)
		{
			_blackOverlay.color = new Color(_blackOverlay.color.r, _blackOverlay.color.g, _blackOverlay.color.b, Mathf.Lerp(0.0F, 1.0F, timeTaken / 2.0F));
			timeTaken += Time.deltaTime;
			yield return null;
		}

		_blackOverlay.color = new Color(_blackOverlay.color.r, _blackOverlay.color.g, _blackOverlay.color.b, 1.0F);
		SceneManager.LoadScene(newScene);
	}
}
