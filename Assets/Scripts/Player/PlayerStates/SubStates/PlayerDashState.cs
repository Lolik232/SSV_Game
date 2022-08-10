using System;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    public Boolean CanDash { get; private set; }

    private Boolean _isGrounded;

    private Boolean _isHolding;
    private Boolean _dashInputStop;

    private Single _lastDashTime;

    private Vector2 _dashDirection;
    private Vector2 _dashDirectionInput;

    private Vector2 _lastAfterImagePosition;

    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isGrounded = _player.CheckIfGrounded();
    }

    public override void Enter()
    {
        base.Enter();

        CanDash = false;
        _player.InputHandler.UseDashInput();

        _isHolding = true;
        _dashDirection = Vector2.right * _player.FacingDirection;

        Time.timeScale = _playerData.holdTimeSccale;
        _startTime = Time.unscaledTime;

        _player.Anim.SetBool("applyDash", false);
        _player.DashDirectionIndicator.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        if (_player.CurrentVelocity.y > 0f)
        {
            _player.SetVelocityY(_player.CurrentVelocity.y * _playerData.dashEndYMultiplier);
        }

        _player.DashDirectionIndicator.gameObject.SetActive(false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isExitingState)
        {
            return;
        }

        if (_isHolding)
        {
            _dashDirectionInput = _player.InputHandler.DashDirectionInput;
            _dashInputStop = _player.InputHandler.DashInputStop;

            if (_isGrounded)
            {
                _player.SetVelocityX(0f);
            }

            if (_dashDirectionInput != Vector2.zero)
            {
                _dashDirection = _dashDirectionInput.normalized;
            }

            Single angle = Vector2.SignedAngle(Vector2.right * _player.FacingDirection, _dashDirection);
            _player.DashDirectionIndicator.rotation = Quaternion.Euler(0f, 0f, angle);
            if (_dashInputStop || Time.unscaledTime >= _startTime + _playerData.maxHoldTime)
            {
                _isHolding = false;
                Time.timeScale = 1f;

                _startTime = Time.time;
                _player.CheckIfShouldFlip(Mathf.RoundToInt(_dashDirection.x));
                _player.Rigidbody.drag = _playerData.drag;
                _player.SetVelocity(_playerData.dashVelocity, _dashDirection);

                _player.Anim.SetBool("applyDash", true);
            }
        }
        else
        {
            _player.SetVelocity(_playerData.dashVelocity, _dashDirection);

            if (Time.time >= _startTime + _playerData.dashTime)
            {
                _player.Rigidbody.drag = 0f;
                _isAbilityDone = true;
                _lastDashTime = Time.time;
            }
        }
    }

    //TODO:  place after image

    private void PlaceAfterImage()
    {
        
    }

    public Boolean CheckIfCanDash()
    {
        return CanDash && Time.time >= _lastDashTime + _playerData.dashCooldown;
    }

    public void ResetCanDash() => CanDash = true;
}
