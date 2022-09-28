using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSubStateSO : SubStateSO
{
    protected Player Player { get; private set; }

    public void Initialize(Player player, StateMachine stateMachine, Animator animator)
    {
        InitializeMachine(stateMachine);
        InitializeAnimator(animator);
        Player = player;
    }
}
