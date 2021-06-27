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
}
