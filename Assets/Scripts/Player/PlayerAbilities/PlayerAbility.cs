using System;
using UnityEngine;

public class PlayerAbility
{
    protected readonly Player Player;
    protected PlayerData Data { get; private set; }

    protected PlayerAbilitiesManager AbilitiesManager;

    public PlayerAbility(PlayerAbilitiesManager abilitiesManager, Player player, PlayerData data)
    {
        AbilitiesManager = abilitiesManager;
        Player = player;
        Data = data;
    }

    public virtual void Initialize()
    {

    }
}
