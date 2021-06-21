using UnityEngine;

public class LennyAnimationEvents : MonoBehaviour
{
	public void JumpInitialized()
	{
		LennyManager.Instance.JumpInitialized = true;
	}
}
