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

	public static void DoAfter(this MonoBehaviour monoBehaviour, Func<bool> triggerCondition, Action callback)
	{
		monoBehaviour.StartCoroutine(TriggeredResponseCoroutine(triggerCondition, callback));
	}

	public static void DoWhile(this MonoBehaviour monoBehaviour, Func<bool> continueCondition, Action loopAction)
	{
		monoBehaviour.StartCoroutine(WhileConditionCoroutine(continueCondition, loopAction));
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

	private static IEnumerator TriggeredResponseCoroutine(Func<bool> triggerCondition, Action callback)
	{
		yield return new WaitUntil(() => triggerCondition.Invoke());
		callback.Invoke();
	}

	private static IEnumerator WhileConditionCoroutine(Func<bool> continueCondition, Action loopAction)
	{
		while(continueCondition.Invoke())
		{
			loopAction.Invoke();
			yield return null;
		}
	}
}
