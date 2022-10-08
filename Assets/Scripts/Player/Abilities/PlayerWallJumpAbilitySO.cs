using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PayerWallJumpAbility", menuName = "Abilities/Player/Wall Jump")]

public class PlayerWallJumpAbilitySO : PlayerAbilitySO
{
    [SerializeField] private int _force;
    public int Force => _force;

    [SerializeField] private Vector2 _angle;
    public Vector2 Angle => _angle;

    [SerializeField] private float _coyoteTime;
    private float _startCoyoteTime;
    public bool CoyoteTime => Time.time < _startCoyoteTime + _coyoteTime;

    [SerializeField] private float _wallExitTime;
    public bool NeedHardExit() => Time.time > startTime + _wallExitTime;

    protected override void OnEnable()
    {
        base.OnEnable();

        conditions.Add(() => Player.jumpInput && (Player.isTouchingWall ^ Player.isTouchingWallBack));

        useActions.Add(() =>
        {
            Player.jumpInput = false;
        });

        updateActions.Add(() =>
        {
            IsActive &= Time.time < startTime + duration;
        });
    }

    public void StartCoyoteTime() => _startCoyoteTime = Time.time;
}
