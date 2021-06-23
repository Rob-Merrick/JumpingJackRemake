using System.Collections;
using UnityEngine;

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

	public void FlashScreen()
	{
		StartCoroutine(FlashScreenCoroutine(_whiteScreenFlashColor, 0.15F));
	}

	public void FlashScreenPink()
	{
		StartCoroutine(FlashScreenCoroutine(_pinkScreenFlashColor, 0.15F));
	}

	private IEnumerator FlashScreenCoroutine(Color screenColor, float flashTime)
	{
		Time.timeScale = 0.1F;
		Color previousColor = Camera.main.backgroundColor;
		Camera.main.backgroundColor = screenColor;

		foreach(Hole hole in HoleManager.Instance.Holes)
		{
			hole.GetComponent<SpriteRenderer>().color = screenColor;
		}

		for(int i = 0; i < 2; i++)
		{
			yield return new WaitForSecondsRealtime(flashTime);
		}

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
