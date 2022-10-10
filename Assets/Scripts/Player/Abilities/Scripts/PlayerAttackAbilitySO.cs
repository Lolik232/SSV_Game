using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PayerAttackAbility", menuName = "Player/Abilities/Attack")]

public class PlayerAttackAbilitySO : PlayerAbilitySO
{
    [SerializeField] private float _slashAngle;
    public float SlashAngle => _slashAngle;

    protected override void OnEnable()
    {
        base.OnEnable();

        conditions.Add(() => Player.attackInput);

        useActions.Add(() =>
        {
            isAble = true;
            Player.attackInput = false;
        });
    }
}
