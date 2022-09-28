using System;
using System.Collections.Generic;

using All.BaseClasses;
using All.Events;

using UnityEngine;
using UnityEngine.Events;

public abstract class StateSO : StateBaseSO
{
    [SerializeField] private string _animBoolName;

    private Animator _anim;

    protected override void OnEnable()
    {
        base.OnEnable();
        enterActions.Add(() => { _anim.SetBool(_animBoolName, true); });
        exitActions.Add(() => { _anim.SetBool(_animBoolName, false); });
    }

    protected void InitializeAnimator(Animator animator) => _anim = animator;

    protected void SetBool(string name, bool value) => _anim.SetBool(name, value);
    protected void SetInteger(string name, int value) => _anim.SetInteger(name, value);
    protected void SetFloat(string name, float value) => _anim.SetFloat(name, value);
}

