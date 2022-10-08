using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PayerJumpAbility", menuName = "Abilities/Player/Jump")]

public class PlayerJumpAbilitySO : PlayerAbilitySO
{
    [SerializeField] private int _force;
    public int Force => _force;

    [SerializeField] private float _coyoteTime;

     private float _startCoyoteTime;

    public bool CoyoteTime => Time.time < _startCoyoteTime + _coyoteTime;

    protected override void OnEnable()
    {
        base.OnEnable();

        conditions.Add(() => Player.jumpInput && !Player.isTouchingCeiling);

        useActions.Add(() =>
        {
            Player.jumpInput = false;
        });

        updateActions.Add(() =>
        {
            IsActive &= Player.Velocity.y > 0f;
        });
    }

    public void StartCoyoteTime() => _startCoyoteTime = Time.time;
}
