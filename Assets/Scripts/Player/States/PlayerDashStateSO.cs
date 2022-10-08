using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDashState", menuName = "State Machine/States/Player/Ability States/Dash")]

public class PlayerDashStateSO : PlayerAbilityStateSO
{
    [Header("Connected Ability")]
    [SerializeField] private PlayerDashAbilitySO _dashAbility;

    protected override void OnEnable()
    {
        base.OnEnable();

        enterActions.Add(() =>
        {
            Player.SetVelocity(_dashAbility.Force * _dashAbility.Angle);
            int xDirection = _dashAbility.Angle.x >= 0 ? 1 : -1;
            Player.CheckIfShouldFlip(xDirection);
            Player.EnableTrail();
        });

        updateActions.Add(() =>
        {
            abilityDone = !_dashAbility.IsActive;
        });

        exitActions.Add(() =>
        {
            Player.DisableTrail();
            if (Player.Velocity.y > 0f)
            {
                Player.SetVelocityY(Player.Velocity.y * 0.2f);
            }
        });
    }
}
