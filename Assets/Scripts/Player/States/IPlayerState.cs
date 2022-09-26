using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState : IState
{
    void Initialize(Player player, StateMachine stateMachine);
}
