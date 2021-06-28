using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseLifeScreen3D : MonoBehaviour
{
    [SerializeField] private Image _loseLifePrefab;
	[SerializeField] private GridLayoutGroup _gridLayoutGroup;

	private readonly List<Image> _loseLifeImages = new List<Image>();

	private void Initialize()
	{
		gameObject.SetActive(true);
		_gridLayoutGroup.enabled = true;

		for(int i = 0; i < _loseLifeImages.Count; i++)
		{
			Destroy(_loseLifeImages[i].gameObject);
		}

		_loseLifeImages.Clear();

		for(int i = 0; i < LennyManager3D.Instance.Lives + 1; i++)
		{
			Image loseLifeImage = Instantiate(_loseLifePrefab);
			loseLifeImage.transform.SetParent(transform, worldPositionStays: false);
			_loseLifeImages.Add(loseLifeImage);
		}

		this.DoAfterFrames(3, () => _gridLayoutGroup.enabled = false);
	}

	private void LastLifeFalls()
	{
		Image lastImage = _loseLifeImages[_loseLifeImages.Count - 1];
		lastImage.GetComponent<Animator>().SetOnlyTrigger("Fall");

		this.DoWhile(() => lastImage.rectTransform.position.y > -1080, () =>
		{
			lastImage.rectTransform.position = new Vector3(lastImage.rectTransform.position.x, lastImage.rectTransform.position.y - 360.0F * Time.deltaTime, lastImage.rectTransform.position.z);
		});

		this.DoAfter(seconds: 1.0F, () => StartCoroutine(FadeDown()));
	}

	private IEnumerator FadeUp()
	{
		float timeTaken = 0.0F;

		while(Application.isPlaying && timeTaken < 2.0F)
		{
			for(int i = 0; i < _loseLifeImages.Count; i++)
			{
				Image loseLifeImage = _loseLifeImages[i];
				loseLifeImage.color = new Color(loseLifeImage.color.r, loseLifeImage.color.g, loseLifeImage.color.b, Mathf.Lerp(0.0F, 1.0F, timeTaken / 2.0F));
			}

			timeTaken += Time.deltaTime;
			yield return null;
		}

		for(int i = 0; i < _loseLifeImages.Count; i++)
		{
			Image loseLifeImage = _loseLifeImages[i];
			loseLifeImage.color = new Color(loseLifeImage.color.r, loseLifeImage.color.g, loseLifeImage.color.b, 1.0F);
		}
	}

	private IEnumerator FadeDown()
	{
		float timeTaken = 0.0F;

		while(Application.isPlaying && timeTaken < 2.0F)
		{
			for(int i = 0; i < _loseLifeImages.Count; i++)
			{
				Image loseLifeImage = _loseLifeImages[i];
				loseLifeImage.color = new Color(loseLifeImage.color.r, loseLifeImage.color.g, loseLifeImage.color.b, Mathf.Lerp(1.0F, 0.0F, timeTaken / 2.0F));
			}

			timeTaken += Time.deltaTime;
			yield return null;
		}

		for(int i = 0; i < _loseLifeImages.Count; i++)
		{
			Image loseLifeImage = _loseLifeImages[i];
			loseLifeImage.color = new Color(loseLifeImage.color.r, loseLifeImage.color.g, loseLifeImage.color.b, 0.0F);
		}

		gameObject.SetActive(false);
	}

	public void Display()
	{
		Initialize();
		StartCoroutine(FadeUp());
		this.DoAfter(seconds: 2.0F, LastLifeFalls);
	}
}
