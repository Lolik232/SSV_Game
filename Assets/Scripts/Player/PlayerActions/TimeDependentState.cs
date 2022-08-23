using System;

using UnityEngine;

public class TimeDependentState : TriggerState
{
    public Boolean IsSensitiveToScaledTime { get; }
    public Single Duration { get; protected set; }
    public Single StartTime { get; private set; }
    public Single EndTime { get => StartTime + Duration; }

    public TimeDependentState(Single duration, bool isSensitiveToScaledTime = true)
    {
        Duration = duration;
        IsSensitiveToScaledTime = isSensitiveToScaledTime;
    }

    public override void Terminate() => base.Terminate();

    public override void Initiate()
    {
        base.Initiate();
        StartTime = GetTime();
    }

    public virtual Boolean IsOutOfTime() => GetTime() > EndTime;

    protected Single GetTime() => IsSensitiveToScaledTime ? GetScaledTime() : GetUnscaledTime();

    private Single GetScaledTime() => Time.time;

    private Single GetUnscaledTime() => Time.unscaledTime;
}
