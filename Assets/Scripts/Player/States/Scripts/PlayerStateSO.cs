using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class PlayerStateSO : StateSO
{
    protected Player Player { get; private set; }

    public void Initialize(Player player)
    {
        InitializeMachine(player.Machine);
        InitializeAnimator(player.Anim);
        Player = player;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        checks.Add(()=>
        {
            Player.DoChecks();
        });
    }
}
