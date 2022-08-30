using System;

using UnityEngine;

public class PlayerAbilitiesManager
{
    public PlayerData Data { get; private set; }

    private readonly Player Player;

    public PlayerJumpAbility JumpAbility { get; private set; }
    public PlayerWallClimbAbility WallClimbAbility { get; private set; }

    public PlayerAbilitiesManager(Player player, PlayerData data)
    {
        Player = player;
        Data = data;

        JumpAbility = new PlayerJumpAbility(this, player, data);
        WallClimbAbility = new PlayerWallClimbAbility(this, player, data);
    }

    public void Initialize()
    {
        JumpAbility.Initialize();
    }

}
