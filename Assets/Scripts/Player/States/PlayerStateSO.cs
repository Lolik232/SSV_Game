using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class PlayerStateSO : StateSO
{
    protected Player Player { get; private set; }

    public void Initialize(Player player, StateMachine stateMachine, Animator animator)
    {
        InitializeMachine(stateMachine);
        InitializeAnimator(animator);
        Player = player;
    }
}
