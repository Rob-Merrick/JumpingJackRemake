using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : Manager<ScreenManager>
{
	[SerializeField] private int _worldLeftEdge;
	[SerializeField] private int _worldRightEdge;
	[SerializeField] private int _worldBottomEdge;
	[SerializeField] private int _worldTopEdge;
	[SerializeField] private int _playableAreaLeftEdge;
	[SerializeField] private int _playableAreaRightEdge;
	[SerializeField] private int _playableAreaBottomEdge;
	[SerializeField] private int _playableAreaTopEdge;
	[SerializeField] private Color _whiteScreenFlashColor;
	[SerializeField] private Color _pinkScreenFlashColor;
	[SerializeField] private GameObject _blackScreenFade;

	public int  WorldLeftEdge => _worldLeftEdge;
	public int  WorldRightEdge => _worldRightEdge;
	public int  WorldBottomEdge => _worldBottomEdge;
	public int  WorldTopEdge => _worldTopEdge;
	public int  WorldHorizontalDistance => _worldRightEdge - _worldLeftEdge;
	public int  WorldVerticalDistance => _worldTopEdge - _worldBottomEdge;

	public int  PlayableAreaLeftEdge => _playableAreaLeftEdge;
	public int  PlayableAreaRightEdge => _playableAreaRightEdge;
	public int  PlayableAreaBottomEdge => _playableAreaBottomEdge;
	public int  PlayableAreaTopEdge => _playableAreaTopEdge;
	public int  PlayableAreaHorizontalDistance => _playableAreaRightEdge - _playableAreaLeftEdge;
	public int  PlayableAreaVerticalDistance => _playableAreaTopEdge - _playableAreaBottomEdge;

	private void Start()
	{
		VerifyEdges();
	}

	public void FadeToBlack()
	{
		StartCoroutine(FadeToBlackCoroutine());
	}

	public void StopFade()
	{
		_blackScreenFade.GetComponent<Image>().color = new Color(0.0F, 0.0F, 0.0F, 0.0F);
	}

	public void FlashScreen()
	{
		StartCoroutine(FlashScreenCoroutine(_whiteScreenFlashColor, 0.15F, SoundManager.Instance.GetAudioSourceByName("HitHead")));
	}

	public void FlashScreenPink()
	{
		StartCoroutine(FlashScreenCoroutine(_pinkScreenFlashColor, 0.15F, SoundManager.Instance.GetAudioSourceByName("HazardHit")));
	}

	private IEnumerator FadeToBlackCoroutine()
	{
		Image blackImage = _blackScreenFade.GetComponent<Image>();

		while(blackImage.color.a < 1.0F)
		{
			blackImage.color = new Color(blackImage.color.r, blackImage.color.g, blackImage.color.b, blackImage.color.a + Time.deltaTime);
			yield return null;
		}

		blackImage.color = new Color(blackImage.color.r, blackImage.color.g, blackImage.color.b, 1.0F);
	}

	private IEnumerator FlashScreenCoroutine(Color screenColor, float flashTime, AudioSource sound)
	{
		sound.Play();
		Time.timeScale = 0.1F;
		Color previousColor = Camera.main.backgroundColor;
		Camera.main.backgroundColor = screenColor;

		foreach(Hole hole in HoleManager.Instance.Holes)
		{
			hole.GetComponent<SpriteRenderer>().color = screenColor;
		}

		yield return new WaitForSecondsRealtime(flashTime);
		Camera.main.backgroundColor = previousColor;

		foreach(Hole hole in HoleManager.Instance.Holes)
		{
			hole.GetComponent<SpriteRenderer>().color = previousColor;
		}

		Time.timeScale = 1.0F;
	}

	private void VerifyEdges()
	{
		if(WorldHorizontalDistance <= 0)
		{
			throw new System.Exception($"{nameof(WorldLeftEdge)} must be less than {nameof(WorldRightEdge)}");
		}

		if(WorldVerticalDistance <= 0)
		{
			throw new System.Exception($"{nameof(WorldTopEdge)} must be less than {nameof(WorldBottomEdge)}");
		}

		if(PlayableAreaHorizontalDistance <= 0)
		{
			throw new System.Exception($"{nameof(PlayableAreaLeftEdge)} must be less than {nameof(PlayableAreaRightEdge)}");
		}

		if(PlayableAreaVerticalDistance <= 0)
		{
			throw new System.Exception($"{nameof(PlayableAreaTopEdge)} must be less than {nameof(PlayableAreaBottomEdge)}");
		}
	}
}
