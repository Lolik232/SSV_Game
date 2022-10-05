using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDashState", menuName = "State Machine/States/Player/Ability States/Dash")]

public class PlayerDashStateSO : PlayerAbilityStateSO
{
    protected override void OnEnable()
    {
        base.OnEnable();

        enterActions.Add(() =>
        {
            isGroundedTransitionBlock = true;
            Player.SetVelocity(Player.DashForce * Player.dashDirection);
            int xDirection = Player.dashDirection.normalized.x >= 0 ? 1 : -1;
            Player.CheckIfShouldFlip(xDirection);
            Player.dashInput = false;
            Player.dash = true;
            Player.dashStartTime = startTime;
            Player.EnableTrail();
        });

        updateActions.Add(() =>
        {
            abilityDone = !Player.dash;
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
