using System;

using UnityEngine;

public class PlayerAbilitiesManager
{
    public PlayerData Data { get; private set; }

    public PlayerStatesManager StatesManager { get; private set; }


    private readonly Player m_Player;

    public PlayerJumpAbility JumpAbility { get; private set; }

    public PlayerAbilitiesManager(Player player)
    {
        m_Player = player;
    }

    public void SetDependencies()
    {
        Data = m_Player.Data;
        StatesManager = m_Player.StatesManager;

        JumpAbility = new PlayerJumpAbility(this);
    }

    public void Initialize()
    {
        JumpAbility.Initialize();
    }

}
