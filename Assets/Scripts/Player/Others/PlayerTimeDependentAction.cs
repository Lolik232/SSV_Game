using System;

using UnityEngine;

public class PlayerTimeDependentAction : PlayerAction
{
    public Boolean IsSensitiveToScaledTime { get; protected set; }

    public Single Duration { get; protected set; }

    public Single StartTime { get; private set; }

    public Single EndTime { get => StartTime + Duration; }

    public PlayerTimeDependentAction(Single duration, bool isSensitiveToScaledTime = true)
    {
        Duration = duration;
        IsSensitiveToScaledTime = isSensitiveToScaledTime;
    }

    public override void End()
    {
        base.End();
    }

    public override void Start()
    {
        base.Start();

        StartTime = GetTime();
    }

    public virtual Boolean IsOutOfTime()
    {
        return GetTime() > EndTime;
    }

    protected Single GetTime()
    {
        return IsSensitiveToScaledTime ? GetScaledTime() : GetUnscaledTime();
    }

    private Single GetScaledTime()
    {
        return Time.time;
    }

    private Single GetUnscaledTime()
    {
        return Time.unscaledTime;
    }
}
