using UnityEngine;

[RequireComponent(typeof(PlayerGroundedState), typeof(PlayerInAirState), typeof(PlayerTouchingWallState))]
[RequireComponent(typeof(PlayerOnLedgeState))]
[RequireComponent(typeof(PlayerInputReader))]
[RequireComponent(typeof(PlayerMoveHorizontalAbility), typeof(PlayerMoveVerticalAbility))]
[RequireComponent(typeof(PlayerLedgeClimbAbility))]
[RequireComponent(typeof(PlayerCrouchAbility))]
[RequireComponent(typeof(PlayerJumpAbility))]

public class Player : MonoBehaviour
{

}
