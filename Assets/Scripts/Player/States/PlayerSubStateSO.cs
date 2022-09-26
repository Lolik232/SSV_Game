using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSubStateSO : SubStateSO, IPlayerState
{
    protected Player Player { get; private set; }

    public void Initialize(Player player, StateMachine stateMachine)
    {
        Initialize(stateMachine);
        Player = player;
    }
}
