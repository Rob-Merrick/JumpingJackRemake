using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LennyAnimationEvents3D : MonoBehaviour
{
	public void JumpBeginVerticalAscent()
	{
		LennyManager3D.Instance.JumpBeginVerticalAscent = true;
		LennyManager3D.Instance.JumpApex = false;
		LennyManager3D.Instance.JumpEndVerticalDescent = false;
	}

	public void JumpApex()
	{
		LennyManager3D.Instance.JumpBeginVerticalAscent = false;
		LennyManager3D.Instance.JumpApex = true;
		LennyManager3D.Instance.JumpEndVerticalDescent = false;
	}

	public void JumpEndVerticalDescent()
	{
		LennyManager3D.Instance.JumpBeginVerticalAscent = false;
		LennyManager3D.Instance.JumpApex = false;
		LennyManager3D.Instance.JumpEndVerticalDescent = true;
	}

	public void PlayFootstepSound()
	{
		SoundManager3D.Instance.PlaySound("Footstep");
	}

	public void PlayHurtSound()
	{
		SoundManager3D.Instance.PlaySound(Random.Range(0.0F, 1.0F) < 0.5F ? "Hurt1" : "Hurt2");
	}

	public void PlayHitHeadSound()
	{
		SoundManager3D.Instance.PlaySound("HitHead");
		this.DoAfter(seconds: Random.Range(0.1F, 0.25F), () => PlayHurtSound());
	}

	public void PlayDizzySound()
	{
		SoundManager3D.Instance.PlaySound("Dizzy");
	}
}
