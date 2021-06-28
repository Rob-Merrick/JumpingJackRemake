using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownAnimationEvents : MonoBehaviour
{
	public void PlayThreeSound()
	{
		SoundManager3D.Instance.PlaySound("3");
	}

	public void PlayTwoSound()
	{
		SoundManager3D.Instance.PlaySound("2");
	}

	public void PlayOneSound()
	{
		SoundManager3D.Instance.PlaySound("1");
	}

	public void PlayGoSound()
	{
		SoundManager3D.Instance.PlaySound("Go");
	}
}
