using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeSelector : MonoBehaviour
{
	public void ReimaginedMode()
	{
		SceneManager.LoadScene("ReimaginedMode");
	}

	public void ClassicMode()
	{
		SceneManager.LoadScene("ClassicMode");
	}
}
