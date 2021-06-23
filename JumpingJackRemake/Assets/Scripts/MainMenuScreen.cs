using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScreen : MonoBehaviour
{
    public void MenuLoaded()
	{
		GameManager.Instance.MainMenuLoaded();
	}
}
