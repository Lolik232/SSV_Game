using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PayerWallJumpAbility", menuName = "Player/Abilities/Wall Jump")]

public class PlayerWallJumpAbilitySO : PlayerAbilitySO
{
    [SerializeField] private int _force;
    [SerializeField] private Vector2 _angle;
    [SerializeField] private float _coyoteTime;

    private float _startCoyoteTime;
    public bool CoyoteTime => Time.time < _startCoyoteTime + _coyoteTime;

    [SerializeField] private float _wallExitTime;
    public bool NeedHardExit() => Time.time > startTime + _wallExitTime;

    protected override void OnEnable()
    {
        base.OnEnable();

        useConditions.Add(() => Player.jumpInput && !Player.isClampedBetweenWalls && (Player.isTouchingWall || Player.isTouchingWallBack));
        terminateConditions.Add(() => Mathf.Abs(Player.Rb.velocity.x) <= 0.01f);

        useActions.Add(() =>
        {
            Player.HoldVelocity(_force, _angle, Player.wallDirection);
            Player.CheckIfShouldFlip(Player.wallDirection);
            Player.jumpInput = false;
        });

        terminateActions.Add(() =>
        {
            Player.ReleaseVelocity();
        });
    }

    public void StartCoyoteTime() => _startCoyoteTime = Time.time;
}
