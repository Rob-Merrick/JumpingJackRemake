using UnityEngine;

public class LennyAnimationEvents : MonoBehaviour
{
	private void JumpInitialized()
	{
		LennyManager.Instance.JumpInitialized();
	}
}
