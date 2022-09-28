using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerJumpState", menuName = "State Machine/States/Player/Sub States/Jump")]

public class PlayerJumpStateSO : PlayerAbilityStateSO
{
    protected override void OnEnable()
    {
        base.OnEnable();

        enterActions.Add(() =>
        {
            OnAbillityDone();
            BlockIsGroundedTransition();
            Player.SetVelocityY(Player.JumpForce);
            Player.jumpInput = false;
            SetBool("isJumping", true);
        });

        exitActions.Add(() => { SetBool("isJumping", false); });
    }
}
