using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public interface IState
{
    void OnStateEnter();
    void OnStateExit();
    void OnUpdate();
    void OnFixedUpdate();
    void OnAnimationFinishTrigger();
    void OnAnimationTrigger();
}
