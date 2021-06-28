using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCountAnimationEVents : MonoBehaviour
{
	public void PlayClickSound()
	{
		SoundManager3D.Instance.PlaySound("Click");
	}
}
