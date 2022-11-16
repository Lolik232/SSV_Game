using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : Ability
{
    public Player Player
    {
        get;
        private set;
    }

    protected override void Awake()
    {
        base.Awake();
        Player = GetComponent<Player>();
    }
}
