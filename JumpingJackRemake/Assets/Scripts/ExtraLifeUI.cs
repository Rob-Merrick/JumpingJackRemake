using UnityEngine;

public class ExtraLifeUI : MonoBehaviour
{
	[SerializeField] private bool _isShownOnScreenDisplay = false;

	public bool IsShownOnScreenDisplay => _isShownOnScreenDisplay;
}
