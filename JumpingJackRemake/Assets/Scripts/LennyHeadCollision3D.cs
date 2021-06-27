using UnityEngine;

public class LennyHeadCollision3D : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<DiscMesh>() != null)
		{
			LennyManager3D.Instance.IsHeadCollided = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.GetComponent<DiscMesh>() != null)
		{
			LennyManager3D.Instance.IsHeadCollided = false;
		}
	}
}
