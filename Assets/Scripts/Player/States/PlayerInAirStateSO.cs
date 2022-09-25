using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInAirState", menuName = "State Machine/States/Player/Sub States/In Air")]
public class PlayerInAirStateSO : PlayerSubStateSO
{
    private bool _isGrounded;

    [SerializeField] private PlayerLandStateSO _toLandState;

    public override void OnStateEnter()
    {
        base.OnStateEnter();
    }

    protected override void DoChecks()
    {
        base.DoChecks();
        _isGrounded = Player.CheckIfGrounded();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        Player.CheckIfShouldFlip(Player.MoveInput.x);
        Player.SetVelocityX(Player.MoveInput.x * Player.InAirMoveSpeed);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        transitions.Add(new TransitionItem(_toLandState, () => _isGrounded));
    }
}
