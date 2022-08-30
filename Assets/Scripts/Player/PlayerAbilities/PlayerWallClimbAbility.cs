using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimbAbility : PlayerAbility
{
    public float ClimbVelocity;
    public float SlideVelocity;

    public bool CanClimb => Player.CharacteristicsManager.Endurance.Current >= m_MinClimbEndurance;
    public bool CanGrab => Player.CharacteristicsManager.Endurance.Current >= m_MinGrabEndurance;

    private float m_MinClimbEndurance;
    private float m_MinGrabEndurance;

    public PlayerWallClimbAbility(PlayerAbilitiesManager abilitiesManager, Player player, PlayerData data) : base(abilitiesManager, player, data)
    {
        ClimbVelocity = data.wallClimbVelocity;
        SlideVelocity = data.wallSlideVelocity;

        m_MinClimbEndurance = data.minClimbEndurance;
        m_MinGrabEndurance = data.minGrabEndurance;
    }

    public override void Initialize()
    {
        base.Initialize();
    }
}
