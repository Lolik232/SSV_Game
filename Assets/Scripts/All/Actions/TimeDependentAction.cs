using System;
using System.Collections;

using UnityEngine;

public class TimeDependentAction : TriggerAction
{
    public readonly bool IsSensitiveToScaledTime;
    public float Duration { get; protected set; }

    private Coroutine m_Coroutine;

    public TimeDependentAction(float duration, bool isSensitiveToScaledTime = true) : base()
    {
        Duration = duration;
        IsSensitiveToScaledTime = isSensitiveToScaledTime;
    }

    public override void Initiate()
    {
        base.Initiate();
        m_Coroutine = CoroutineManager.StartRoutine(WaitForEndTime());
    }

    public override void Terminate()
    {
        base.Terminate();
        CoroutineManager.StopRoutine(m_Coroutine);
    }

    private IEnumerator WaitForEndTime()
    {
        yield return new WaitForSeconds(Duration);
        Terminate();
    }
}
