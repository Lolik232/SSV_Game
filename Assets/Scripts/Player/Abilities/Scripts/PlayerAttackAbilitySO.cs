using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PayerAttackAbility", menuName = "Player/Abilities/Attack")]

public class PlayerAttackAbilitySO : PlayerAbilitySO
{

    protected override void OnEnable()
    {
        base.OnEnable();

        useConditions.Add(() => Player.attackInput);

        useActions.Add(() =>
        {
            Player.attackInput = false;
        });
    }
}
