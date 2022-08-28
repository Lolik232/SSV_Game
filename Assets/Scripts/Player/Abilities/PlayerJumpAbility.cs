using System;
using UnityEngine;

public class PlayerJumpAbility
{
    protected PlayerData Data { get; private set; }

    protected PlayerAbilitiesManager AbilitiesManager;

    public PlayerStatesManager StatesManager { get; private set; }

    public Single JumpHeight { get; private set; }

    public Int32 AmountOfJumpsLeft { get; private set; }

    private TriggerAction m_CanJump;
    public Boolean CanJump => AmountOfJumpsLeft > 0 && m_CanJump;

    public PlayerJumpAbility(PlayerAbilitiesManager abilitiesManager)
    {
        AbilitiesManager = abilitiesManager;
        Data = AbilitiesManager.Data;
        StatesManager = AbilitiesManager.StatesManager;

        m_CanJump = new TriggerAction();
    }

    public void Initialize()
    {
        StatesManager.LandState.EnterEvent += ResetAmountOfJumps;
        StatesManager.IdleState.EnterEvent += ResetAmountOfJumps;
        StatesManager.MoveState.EnterEvent += ResetAmountOfJumps;
        StatesManager.InAirState.CoyoteTimeEndEvent += m_CanJump.Terminate;
        StatesManager.JumpState.EnterEvent += DecreaseAmountOfJumpsLeft;
    }

    private void ResetAmountOfJumps()
    {
        m_CanJump.Initiate();
        AmountOfJumpsLeft = Data.amountOfJumps;
    }

    public void DecreaseAmountOfJumpsLeft() => AmountOfJumpsLeft--;
}
