using UnityEngine;

public class MainMenuScreen : MonoBehaviour
{
    public void MenuLoaded()
	{
		GameManager.Instance.MainMenuLoaded();
	}

	public void PlayStartSound()
	{
		SoundManager.Instance.GetAudioSourceByName("Start").Play();
	}
}
