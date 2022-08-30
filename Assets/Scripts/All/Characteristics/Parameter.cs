using System;
using UnityEngine;

public class Parameter
{
    public float Base;

    public float PreAddition;

    public float Modifier;

    public float AfterAddition;

    public float Max => (Base + PreAddition) * Modifier + AfterAddition;

    public float Min => 0;

    public float Current { get; protected set; }

    public Parameter(float @base, float preAddition = 0, float modifier = 1, float afterAddition = 0)
    {
        Base = @base;
        PreAddition = preAddition;
        Modifier = modifier;
        AfterAddition = afterAddition;
        Current = Max;
    }

    public void AddValue(Single value)
    {
        Current = Mathf.Max(Min, Mathf.Min(Max, Current + value));
    }

    public void RemoveValue(Single value)
    {
        Current = Mathf.Max(Min, Mathf.Min(Max, Current - value));
    }

    public void SetValue(Single value)
    {
        Current = Mathf.Max(Min, Mathf.Min(Max, value));
    }

    public bool IsEmpty()
    {
        return Current == Min;
    }

    public bool IsFull()
    {
        return Current == Max;
    }
}
