using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeSelector : MonoBehaviour
{
	public void ReimaginedMode()
	{
		Time.timeScale = 1.0F;
		SceneManager.LoadScene("ReimaginedModeMenu");
	}

	public void ClassicMode()
	{
		Time.timeScale = 1.0F;
		SceneManager.LoadScene("ClassicMode");
	}
}
