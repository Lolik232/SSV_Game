using System;
using System.Collections;

using UnityEngine;

public class TimeDependentAction : TriggerAction
{
    public Boolean IsSensitiveToScaledTime { get; }
    public Single Duration { get; protected set; }

    private Coroutine m_Coroutine;

    public TimeDependentAction(Single duration, Boolean isSensitiveToScaledTime = true) : base()
    {
        Duration = duration;
        IsSensitiveToScaledTime = isSensitiveToScaledTime;
    }

    public override void Terminate()
    {
        base.Terminate();
        CoroutineManager.StopRoutine(m_Coroutine);
    }

    public override void Initiate()
    {
        base.Initiate();
        m_Coroutine = CoroutineManager.StartRoutine(WaitForEndTime());
    }

    private IEnumerator WaitForEndTime()
    {
        yield return new WaitForSeconds(Duration);
        Terminate();
    }
}
