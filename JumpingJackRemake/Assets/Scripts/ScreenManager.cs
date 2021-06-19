using UnityEngine;

public class ScreenManager : MonoBehaviour
{
	[SerializeField] private float _worldLeftEdge;
	[SerializeField] private float _worldRightEdge;
	[SerializeField] private float _worldBottomEdge;
	[SerializeField] private float _worldTopEdge;
	[SerializeField] private float _playableAreaLeftEdge;
	[SerializeField] private float _playableAreaRightEdge;
	[SerializeField] private float _playableAreaBottomEdge;
	[SerializeField] private float _playableAreaTopEdge;

	public static ScreenManager Instance;

	public float WorldLeftEdge => _worldLeftEdge;
	public float WorldRightEdge => _worldRightEdge;
	public float WorldBottomEdge => _worldBottomEdge;
	public float WorldTopEdge => _worldTopEdge;
	public float WorldHorizontalDistance => _worldRightEdge - _worldLeftEdge;
	public float WorldVerticalDistance => _worldTopEdge - _worldBottomEdge;

	public float PlayableAreaLeftEdge => _playableAreaLeftEdge;
	public float PlayableAreaRightEdge => _playableAreaRightEdge;
	public float PlayableAreaBottomEdge => _playableAreaBottomEdge;
	public float PlayableAreaTopEdge => _playableAreaTopEdge;
	public float PlayableAreaHorizontalDistance => _playableAreaRightEdge - _playableAreaLeftEdge;
	public float PlayableAreaVerticalDistance => _playableAreaTopEdge - _playableAreaBottomEdge;

	private void Start()
	{
		if(Instance != null)
		{
			throw new System.Exception($"Attempting to overwrite the singleton instance for {name}");
		}

		Instance = this;
		VerifyEdges();
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
