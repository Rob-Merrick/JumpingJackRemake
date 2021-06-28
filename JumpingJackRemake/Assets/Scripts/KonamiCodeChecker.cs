using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KonamiCodeChecker : Manager<KonamiCodeChecker>
{
	[SerializeField] private GameObject _konamiCodeWindow;
	[SerializeField] private AudioSource _correctInputSound;
	[SerializeField] private AudioSource _incorrectInputSound;
	[SerializeField] private AudioSource _activationSound;

    private readonly KeyCode[] _konamiCode = { KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.RightArrow };
    private int _konamiCodeIndex = 0;
    private float _konamiCodeTimer = 0.0F;

	public static bool IsKonamiCodeEnabled { get; private set; } = false;

	public bool CheckForKonamiCode()
	{
		if(IsKonamiCodeEnabled)
		{
			return false;
		}

		KeyCode? keyHit = GetKeyHit();

		if(_konamiCodeWindow.activeSelf)
		{
			if(Input.GetKeyDown(KeyCode.Return))
			{
				IsKonamiCodeEnabled = true;
				_konamiCodeWindow.SetActive(false);
				Time.timeScale = 1.0F;
				return true;
			}
		}
		else if(keyHit.HasValue)
		{
			if(keyHit.Value == _konamiCode[_konamiCodeIndex])
			{
				if(_correctInputSound != null)
				{
					_correctInputSound.Play();
				}

				_konamiCodeIndex++;
				_konamiCodeTimer = 0.0F;

				if(_konamiCodeIndex >= _konamiCode.Length)
				{
					if(_activationSound != null)
					{
						_activationSound.Play();
					}

					Time.timeScale = 0.0F;
					_konamiCodeWindow.SetActive(true);
				}
			}
			else
			{
				KonamiCodeFailure();
			}
		}
		else if(_konamiCodeIndex > 0 && _konamiCodeTimer >= 2.0F)
		{
			KonamiCodeFailure();
		}
		else
		{
			_konamiCodeTimer += Time.deltaTime;
		}

		return false;
	}

	private void KonamiCodeFailure()
	{
		if(_incorrectInputSound != null)
		{
			_incorrectInputSound.Play();
		}

		_konamiCodeTimer = 0.0F;
		_konamiCodeIndex = 0;
	}

	private KeyCode? GetKeyHit()
	{
		foreach(KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
		{
			if(Input.GetKeyDown(keyCode))
			{
				return keyCode;
			}
		}

		return null;
	}
}
