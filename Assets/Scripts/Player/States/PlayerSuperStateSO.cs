using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class PlayerSuperStateSO : SuperStateSO
{
    protected Player Player { get; private set; }

    public void Initialize(Player player, StateMachine stateMachine)
    {
        InitializeMachine(stateMachine);
        Player = player;
    }
}
