using UnityEngine;

public class LennyHazardCollider3D : MonoBehaviour
{
	[SerializeField] private GameObject _front;
	[SerializeField] private GameObject _back;

	private void OnTriggerEnter(Collider other)
	{
		Hazard3D hazard = other.gameObject.GetComponent<Hazard3D>();

		if(hazard != null && !LennyManager3D.Instance.IsHit)
		{
			float distanceToFront = Vector3.Distance(hazard.gameObject.transform.position, _front.transform.position);
			float distanceToBack = Vector3.Distance(hazard.gameObject.transform.position, _back.transform.position);
			string trigger = distanceToFront < distanceToBack ? "HitFromFront" : "HitFromBack";
			LennyManager3D.Instance.Animator.SetOnlyTrigger(trigger);
		}
	}
}
