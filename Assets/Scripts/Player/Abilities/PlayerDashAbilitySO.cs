using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "PayerDashAbility", menuName = "Abilities/Player/Dash")]

public class PlayerDashAbilitySO : PlayerAbilitySO
{
    [SerializeField] private int _force;
    public int Force => _force;

    public Vector2 Angle => Player.dashDirection;

    protected override void OnEnable()
    {
        base.OnEnable();

        conditions.Add(() => Player.dashInput && Angle != Vector2.zero);

        useActions.Add(() =>
        {
            Player.abilityInput = false;
        });

        updateActions.Add(() =>
        {
            IsActive &= Time.time < startTime + duration;
        });
    }
}
