using System;

public enum ScreenRegion
{
	None = 1,
	World = 2,
	PlayableArea = 3,
	Custom = 4
}

public static class ScreenRegionExtensions
{
	public static float GetLeftEdgeValue(this ScreenRegion screenRegion, float customValue)
	{
		switch(screenRegion)
		{
			case ScreenRegion.None:			return float.MinValue;
			case ScreenRegion.World:		return ScreenManager.Instance.WorldLeftEdge;
			case ScreenRegion.PlayableArea:	return ScreenManager.Instance.PlayableAreaLeftEdge;
			case ScreenRegion.Custom:		return customValue;
			default:						throw new NotImplementedException();
		}
	}

	public static float GetRightEdgeValue(this ScreenRegion screenRegion, float customValue)
	{
		switch(screenRegion)
		{
			case ScreenRegion.None:			return float.MaxValue;
			case ScreenRegion.World:		return ScreenManager.Instance.WorldRightEdge;
			case ScreenRegion.PlayableArea:	return ScreenManager.Instance.PlayableAreaRightEdge;
			case ScreenRegion.Custom:		return customValue;
			default:						throw new NotImplementedException();
		}
	}

	public static float GetBottomEdgeValue(this ScreenRegion screenRegion, float customValue)
	{
		switch(screenRegion)
		{
			case ScreenRegion.None:			return float.MinValue;
			case ScreenRegion.World:		return ScreenManager.Instance.WorldBottomEdge;
			case ScreenRegion.PlayableArea:	return ScreenManager.Instance.PlayableAreaBottomEdge;
			case ScreenRegion.Custom:		return customValue;
			default:						throw new NotImplementedException();
		}
	}

	public static float GetTopEdgeValue(this ScreenRegion screenRegion, float customValue)
	{
		switch(screenRegion)
		{
			case ScreenRegion.None:			return float.MinValue;
			case ScreenRegion.World:		return ScreenManager.Instance.WorldTopEdge;
			case ScreenRegion.PlayableArea:	return ScreenManager.Instance.PlayableAreaTopEdge;
			case ScreenRegion.Custom:		return customValue;
			default:						throw new NotImplementedException();
		}
	}

	public static float GetHorizontalDistanceValue(this ScreenRegion screenRegion, float customValue)
	{
		switch(screenRegion)
		{
			case ScreenRegion.None:			return 0.0F;
			case ScreenRegion.World:		return ScreenManager.Instance.WorldHorizontalDistance;
			case ScreenRegion.PlayableArea:	return ScreenManager.Instance.PlayableAreaHorizontalDistance;
			case ScreenRegion.Custom:		return customValue;
			default:						throw new NotImplementedException();
		}
	}

	public static float GetVerticalDistanceValue(this ScreenRegion screenRegion, float customValue)
	{
		switch(screenRegion)
		{
			case ScreenRegion.None:			return 0.0F;
			case ScreenRegion.World:		return ScreenManager.Instance.WorldVerticalDistance;
			case ScreenRegion.PlayableArea:	return ScreenManager.Instance.PlayableAreaVerticalDistance;
			case ScreenRegion.Custom:		return customValue;
			default:						throw new NotImplementedException();
		}
	}
}