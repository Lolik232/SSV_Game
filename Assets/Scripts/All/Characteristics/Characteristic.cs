using System;
using UnityEngine;

public class Characteristic : Parameter
{
    public Parameter Regeneration;

    public Single FatigueRate;

    public Characteristic(float @base, float preAddition = 0, float modifier = 1, float afterAddition = 0, float regenBase = 0, float regenPreAddition = 0, float regenModidifier = 1, float regenAfterAddition = 0) : base(@base, preAddition, modifier, afterAddition)
    {
        Regeneration = new Parameter(regenBase, regenPreAddition, regenModidifier, regenAfterAddition);
    }

    public virtual void LogicUpdate()
    {
        GetTired();
        Regenerate();
    }

    public virtual void PhysicsUpdate()
    {

    }

    protected void Regenerate()
    {
        AddValue(Regeneration.Current * Time.deltaTime);
    }

    public void EnableRegeneration()
    {
        Regeneration.SetValue(Regeneration.Max);
    }

    public void DisableRegeneration()
    {
        Regeneration.SetValue(Regeneration.Min);
    }

    protected void GetTired()
    {
        RemoveValue(FatigueRate * Time.deltaTime);
    }
}
