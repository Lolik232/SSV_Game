using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public interface IState
{
    void InitializeStateMachine(StateMachine stateMachine);

    void OnStateEnter();

    void OnStateExit();

    void OnUpdate();

    void OnFixedUpdate();
}
