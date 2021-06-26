using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LennyManager3D : Manager<LennyManager3D>
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
		{
            _animator.SetOnlyTrigger("Jumping");
		}
    }
}
