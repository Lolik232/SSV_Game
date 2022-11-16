using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerWeapon : Weapon
{
    protected Player Player
    {
        get;
        private set;
    }

    protected override void Awake()
    {
        base.Awake();
        Player = Inventory.GetComponentInParent<Player>();
    }
}
