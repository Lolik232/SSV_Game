using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDashState", menuName = "Player/States/Ability States/Dash")]

public class PlayerDashStateSO : PlayerAbilityStateSO
{
    private float _cachedGravity;

    [Header("Connected Ability")]
    [SerializeField] private PlayerDashAbilitySO _dashAbility;

    protected override void OnEnable()
    {
        base.OnEnable();

        enterActions.Add(() =>
        {
            _cachedGravity = Player.Rb.gravityScale;
            Player.Rb.gravityScale = 0f;
            Player.CheckIfShouldFlip(Player.dashDirection.x >= 0 ? 1 : -1);
            Player.SetVelocity(_dashAbility.Force * Player.dashDirection);
            Player.Tr.emitting = true;
        });

        updateActions.Add(() =>
        {
            abilityDone = !_dashAbility.isActive;
        });

        exitActions.Add(() =>
        {
            Player.Rb.gravityScale = _cachedGravity;
            Player.Tr.emitting = false;
            if (Player.Rb.velocity.y > 0f)
            {
                Player.SetVelocityY(Player.Rb.velocity.y * 0.1f);
            }
        });
    }
}
