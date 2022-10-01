using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerOnLedgeStateSO : PlayerStateSO
{
    protected override void OnEnable()
    {
        base.OnEnable();

        enterActions.Add(() =>
        {
            Player.SetVelocityZero();
        });

        updateActions.Add(() =>
        {
            Player.HoldPosition(Player.ledgeStartPosition);
        });
    }
}
