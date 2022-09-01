using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimbAbility : PlayerAbility
{
    public float ClimbVelocity;
    public float SlideVelocity;

    public bool CanWallClimb => Player.CharacteristicsManager.Endurance.Current >= m_MinWallClimbEndurance;
    public bool CanWallGrab => Player.CharacteristicsManager.Endurance.Current >= m_MinWallGrabEndurance;
    public bool CanLedgeClimb => Player.CharacteristicsManager.Endurance.Current >= LedgeClimbEnduranceCost;

    private float m_MinWallClimbEndurance;
    private float m_MinWallGrabEndurance;

    public float WallClimbEnduranceFatigueRate { get; private set; }
    public float WallGrabEnduranceFatigueRate { get; private set; }
    public float LedgeClimbEnduranceCost { get; private set; }

    public PlayerWallClimbAbility(PlayerAbilitiesManager abilitiesManager, Player player, PlayerData data) : base(abilitiesManager, player, data)
    {
        ClimbVelocity = data.wallClimbVelocity;
        SlideVelocity = data.wallSlideVelocity;

        m_MinWallClimbEndurance = data.minWallClimbEndurance;
        m_MinWallGrabEndurance = data.minWallGrabEndurance;

        WallClimbEnduranceFatigueRate = data.wallClimbEnduranceFatigueRate;
        WallGrabEnduranceFatigueRate = data.wallGrabEnduranceFatigueRate;
        LedgeClimbEnduranceCost = data.ledgeClimbEnduranceCost;
    }

    public override void Initialize()
    {
        base.Initialize();
    }
}
