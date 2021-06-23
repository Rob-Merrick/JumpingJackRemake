using System.Collections.Generic;
using UnityEngine;

public class Lenny : MonoBehaviour
{
	private LennyManager _lennyManager;
	private readonly IDictionary<Collider2D, bool> _collisionEntryExitTracker = new Dictionary<Collider2D, bool>();

	private void Start()
	{
		_lennyManager = LennyManager.Instance;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(_lennyManager.LennyGameObject.transform.position.x <= ScreenManager.Instance.PlayableAreaLeftEdge || _lennyManager.LennyGameObject.transform.position.x >= ScreenManager.Instance.PlayableAreaRightEdge)
		{
			return;
		}

		if(_collisionEntryExitTracker.ContainsKey(collision))
		{
			_collisionEntryExitTracker[collision] = true;
		}
		else
		{
			_collisionEntryExitTracker.Add(collision, true);
		}

		if(collision.gameObject.HasComponent<Hole>())
		{
			_lennyManager.AddActiveHole();
		}
		else if(collision.gameObject.HasComponent<Hazard>())
		{
			_lennyManager.Stun();
		}
		else if(_lennyManager.ActiveHoles == 0)
		{
			_lennyManager.HitHead = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if(!_collisionEntryExitTracker.TryGetValue(collision, out bool isEntered) || !isEntered)
		{
			return;
		}

		if(collision.gameObject.HasComponent<Hole>())
		{
			_lennyManager.RemoveActiveHole();

			if(_lennyManager.ActiveHoles == 0 && _lennyManager.JumpIsGood)
			{
				_lennyManager.JumpIsGood = false;
			}
		}

		_collisionEntryExitTracker.Remove(collision);
	}

	public void Restart()
	{
		_collisionEntryExitTracker.Clear();
	}
}
