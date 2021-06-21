using UnityEngine;

public class Lenny : MonoBehaviour
{
	private LennyManager _lennyManager;

	private void Start()
	{
		_lennyManager = LennyManager.Instance;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
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
		if(collision.gameObject.HasComponent<Hole>())
		{
			_lennyManager.RemoveActiveHole();

			if(_lennyManager.ActiveHoles == 0 && _lennyManager.JumpIsGood)
			{
				_lennyManager.JumpIsGood = false;
			}
		}
	}
}
