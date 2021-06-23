using UnityEngine;

public class LennyAnimationEvents : MonoBehaviour
{
	public void JumpInitialized()
	{
		LennyManager.Instance.JumpInitialized = true;
	}

	public void FlashScreen()
	{
		ScreenManager.Instance.FlashScreen();
	}

	public void PlayIdleSound(string soundName)
	{
		if(GameManager.Instance.IsReady)
		{
			SoundManager.Instance.GetAudioSourceByName(soundName).Play();
		}
	}
}
