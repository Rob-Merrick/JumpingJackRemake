using UnityEngine;

public class Lenny : MonoBehaviour
{
	public int ActiveHoles { get; private set; } = 0;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.HasComponent<Hole>())
		{
			ActiveHoles++;
		}
		else if(collision.gameObject.HasComponent<Hazard>() || ActiveHoles == 0)
		{
			LennyManager.Instance.Stun();
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.gameObject.HasComponent<Hole>())
		{
			ActiveHoles--;
		}
	}
}
