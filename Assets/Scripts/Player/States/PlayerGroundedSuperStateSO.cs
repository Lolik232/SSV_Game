using UnityEngine;


[CreateAssetMenu(fileName = "PlayerGroundedSuperState", menuName = "State Machine/States/Player/Super States/Grounded")]
public class PlayerGroundedSuperStateSO : PlayerSuperStateSO
{
    private bool _isGrounded;

    [SerializeField] private PlayerInAirStateSO _toInAirState;

    protected override void DoChecks()
    {
        base.DoChecks();
        _isGrounded = Player.CheckIfGrounded();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        transitions.Add(new TransitionItem(_toInAirState, () => !_isGrounded));
    }
}
