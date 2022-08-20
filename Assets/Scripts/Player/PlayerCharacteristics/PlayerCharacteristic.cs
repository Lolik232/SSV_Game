using System;
using UnityEngine;

public class PlayerCharacteristic
{
    public Single MaxPoints { get; private set; }
    public Single CurrentPoints { get; private set; }
    public Single BaseRegeneration { get; private set; }
    public Single CurrentRegeneration { get; private set; }

    public PlayerCharacteristic(Single maxPoints, Single regeneration)
    {
        MaxPoints = maxPoints;
        CurrentPoints = MaxPoints;
        BaseRegeneration = regeneration;
        CurrentRegeneration = BaseRegeneration;
    }

    public void ChangePoints(Single points)
    {
        CurrentPoints = Mathf.Min(MaxPoints, Mathf.Max(0f, CurrentPoints + points));
        Debug.Log(CurrentPoints);
        Debug.Log(CurrentRegeneration);

    }

    public void RestorePoints()
    {
        ChangePoints(CurrentRegeneration * Time.deltaTime);
    }


    public void BlockRegeneration()
    {
        SetRegeneration(0f);
    }

    public void SetRegeneration(Single regeneration)
    {
        CurrentRegeneration = regeneration;
    }

    public void ResetRegeneration()
    {
        SetRegeneration(BaseRegeneration);
    }

    public Boolean IsFull()
    {
        return CurrentPoints == MaxPoints;
    }
    public Boolean IsEmpty()
    {
        return CurrentPoints == 0f;
    }
}
