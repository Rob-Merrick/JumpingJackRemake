using System;
using System.Collections;
using UnityEngine;

public static class MonoBehaviourExtensions
{
	public static void DoAfterFrames(this MonoBehaviour monoBehaviour, int frames, Action callback)
	{
		monoBehaviour.StartCoroutine(FrameDelayedCoroutine(frames, callback));
	}

	public static void DoAfter(this MonoBehaviour monoBehaviour, float seconds, Action callback)
	{
		monoBehaviour.StartCoroutine(TimedCoroutine(seconds, callback));
	}

	public static void DoAfter(this MonoBehaviour monoBehaviour, Func<bool> finishedCondition, Action callback)
	{
		monoBehaviour.StartCoroutine(FinishedConditionCoroutine(finishedCondition, callback));
	}

	private static IEnumerator FrameDelayedCoroutine(int frames, Action callback)
	{
		for(int i = 0; i < frames; i++)
		{
			yield return null;
		}

		callback.Invoke();
	}

	private static IEnumerator TimedCoroutine(float seconds, Action callback)
	{
		yield return new WaitForSeconds(seconds);
		callback.Invoke();
	}

	private static IEnumerator FinishedConditionCoroutine(Func<bool> finishedCondition, Action callback)
	{
		yield return new WaitUntil(() => finishedCondition.Invoke());
		callback.Invoke();
	}
}
