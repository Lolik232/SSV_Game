using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "PayerDashAbility", menuName = "Player/Abilities/Dash")]

public class PlayerDashAbilitySO : PlayerAbilitySO
{
    [SerializeField] private int _force;
    public int Force => _force;

    protected override void OnEnable()
    {
        base.OnEnable();

        conditions.Add(() => Player.dashInput && Player.dashDirection != Vector2.zero);

        useActions.Add(() =>
        {
            Player.dashInput = false;
        });

        updateActions.Add(() =>
        {
            isActive &= Time.time < startTime + duration;
        });
    }
}
