using System.Collections.Generic;
using UnityEngine;

public class JumpingJacksUpdate : StateMachineBehaviour
{
	private List<string> _possibleTriggers = new List<string>() { "Idle", "Idle2", "JumpingJacks" };
	private string _newTrigger;
	private float _totalTimeTaken;
	private float _timeToExit;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		foreach(string trigger in _possibleTriggers)
		{
			if(stateInfo.IsName(trigger))
			{
				_possibleTriggers.Remove(trigger);
				break;
			}
		}

		_newTrigger = _possibleTriggers[Random.Range(0, _possibleTriggers.Count)];
		_totalTimeTaken = 0.0F;
		_timeToExit = Random.Range(3.0F, 10.0F);
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		_totalTimeTaken += Time.deltaTime;

		if(_totalTimeTaken >= _timeToExit)
		{
			animator.SetOnlyTrigger(_newTrigger);
		}
	}
}
