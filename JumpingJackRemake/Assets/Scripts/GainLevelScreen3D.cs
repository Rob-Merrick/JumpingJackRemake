using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GainLevelScreen3D : MonoBehaviour
{
	[SerializeField] private GameObject _levelCounterContainer;
	[SerializeField] private Image _whiteOverlay;
	[SerializeField] private TextMeshProUGUI _levelBeforeText;
	[SerializeField] private TextMeshProUGUI _levelAfterText;

	public void Display()
	{
		gameObject.SetActive(true);
		_levelBeforeText.text = $"{GameManager3D.Instance.Level - 1}";
		_levelAfterText.text = $"{GameManager3D.Instance.Level}";
		_levelCounterContainer.SetActive(true);
		StartCoroutine(FadeIn());
		this.DoAfter(seconds: 3.5F, () => StartCoroutine(FadeOut()));
	}

	private IEnumerator FadeIn()
	{
		_whiteOverlay.enabled = true;
		_whiteOverlay.color = new Color(_whiteOverlay.color.r, _whiteOverlay.color.g, _whiteOverlay.color.b, 1.0F);
		float timeTaken = 0.0F;

		while(Application.isPlaying && timeTaken < 0.5F)
		{
			_whiteOverlay.color = new Color(_whiteOverlay.color.r, _whiteOverlay.color.g, _whiteOverlay.color.b, Mathf.Lerp(1.0F, 0.0F, timeTaken / 0.5F));
			timeTaken += Time.deltaTime;
			yield return null;
		}

		_whiteOverlay.color = new Color(_whiteOverlay.color.r, _whiteOverlay.color.g, _whiteOverlay.color.b, 0.0F);
		_whiteOverlay.enabled = false;
	}

	private IEnumerator FadeOut()
	{
		_whiteOverlay.enabled = true;
		_whiteOverlay.color = new Color(_whiteOverlay.color.r, _whiteOverlay.color.g, _whiteOverlay.color.b, 0.0F);
		float timeTaken = 0.0F;

		while(Application.isPlaying && timeTaken < 0.5F)
		{
			_whiteOverlay.color = new Color(_whiteOverlay.color.r, _whiteOverlay.color.g, _whiteOverlay.color.b, Mathf.Lerp(0.0F, 1.0F, timeTaken / 0.5F));
			timeTaken += Time.deltaTime;
			yield return null;
		}

		_whiteOverlay.color = new Color(_whiteOverlay.color.r, _whiteOverlay.color.g, _whiteOverlay.color.b, 1.0F);
		gameObject.SetActive(false);
	}
}
