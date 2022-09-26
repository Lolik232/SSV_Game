using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class PlayerStateSO : StateSO, IPlayerState
{
    protected Player Player { get; private set; }

    public void Initialize(Player player, StateMachine stateMachine)
    {
        Initialize(stateMachine);
        Player = player;
    }
}
