using System;
using UnityEngine;

public class PlayerTimeFloatingAction : TimeDependentState
{
    public readonly Single MinDuration;
    public readonly Single MaxDuration;
    public readonly Single MinEnterDuration;
    public PlayerTimeFloatingAction(Single maxDuration, Single minEnterDuration, bool isSensitiveToScaledTime = true) : base(maxDuration, isSensitiveToScaledTime)
    {
        MaxDuration = maxDuration;
        MinDuration = 0f;
        MinEnterDuration = minEnterDuration;
    }

    public void ChangeDuration(Single time)
    {
        Duration = Mathf.Min(MaxDuration, Mathf.Max(MinDuration, Duration + time));
    }

    public Boolean CanStart()
    {
        return Duration > MinEnterDuration;
    }

    public override Boolean IsOutOfTime()
    {
        return Duration <= MinDuration + 0.01f;
    }
}
