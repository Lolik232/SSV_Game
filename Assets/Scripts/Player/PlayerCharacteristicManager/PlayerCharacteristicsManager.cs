using System;
using UnityEngine;

public class PlayerCharacteristicsManager
{
    private Player Player { get; set; }
    private PlayerData Data { get; set; }

    public Characteristic Endurance { get; protected set; }

    public PlayerCharacteristicsManager(Player player, PlayerData data)
    {
        Player = player;
        Data = data;

        Endurance = new Characteristic(data.baseEndurance, 0, 1, 0, data.baseEnduranceRegeneration, 0, 1, 0);
    }

    public void Initialize()
    {
        Player.StatesManager.JumpState.EnterEvent += OnJump;
        Player.StatesManager.WallJumpState.EnterEvent += OnJump;

        Player.StatesManager.WallGrabState.EnterEvent += OnWallGrabEnter;
        Player.StatesManager.WallSlideState.EnterEvent += OnWallSlideEnter;
        Player.StatesManager.WallClimbState.EnterEvent += OnWallClimbEnter;
        Player.StatesManager.LedgeClimbState.IsClimbing.ActiveEvent += OnLedgeClimbStart;
        Player.StatesManager.LedgeClimbState.EnterEvent += OnLedgeClimbEnter;

        Player.StatesManager.WallGrabState.ExitEvent += OnWallGrabExit;
        Player.StatesManager.WallSlideState.ExitEvent += OnWallSlideExit;
        Player.StatesManager.WallClimbState.ExitEvent += OnWallClimbExit;
        Player.StatesManager.LedgeClimbState.ExitEvent += OnLedgeClimbExit;
    }

    public void LogicUpdate()
    {
        Endurance.LogicUpdate();
        Debug.Log(Endurance.Current);
    }

    public void PhysicsUpdate()
    {
        Endurance.PhysicsUpdate();
    }

    private void OnWallGrabEnter()
    {
        Endurance.DisableRegeneration();
        Endurance.FatigueRate = Player.AbilitiesManager.WallClimbAbility.WallGrabEnduranceFatigueRate;
    }

    private void OnWallGrabExit()
    {
        Endurance.EnableRegeneration();
        Endurance.FatigueRate = 0f;
    }

    private void OnWallClimbEnter()
    {
        Endurance.DisableRegeneration();
        Endurance.FatigueRate = Player.AbilitiesManager.WallClimbAbility.WallClimbEnduranceFatigueRate;
    }

    private void OnWallClimbExit()
    {
        Endurance.EnableRegeneration();
        Endurance.FatigueRate = 0f;
    }

    private void OnWallSlideEnter()
    {
        Endurance.DisableRegeneration();
    }

    private void OnWallSlideExit()
    {
        Endurance.EnableRegeneration();
    }

    private void OnLedgeClimbEnter()
    {
        Endurance.DisableRegeneration();
        Endurance.FatigueRate = Player.AbilitiesManager.WallClimbAbility.WallGrabEnduranceFatigueRate;
    }

    private void OnLedgeClimbStart()
    {
        Endurance.RemoveValue(Player.AbilitiesManager.WallClimbAbility.LedgeClimbEnduranceCost);
    }

    private void OnLedgeClimbExit()
    {
        Endurance.EnableRegeneration();
        Endurance.FatigueRate = 0f;
    }

    private void OnJump()
    {
        Endurance.RemoveValue(Player.AbilitiesManager.JumpAbility.JumpEnduranceCost);
    }
}
