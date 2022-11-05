using UnityEngine;

[RequireComponent(typeof(PlayerGroundedState), typeof(PlayerInAirState), typeof(PlayerTouchingWallState))]
[RequireComponent(typeof(PlayerInputReader))]
[RequireComponent(typeof(GroundChecker), typeof(CeilChecker), typeof(WallChecker))]
[RequireComponent(typeof(LedgeChecker))]
[RequireComponent(typeof(PlayerMoveForwardAbility), typeof(PlayerMoveBackwardAbility), typeof(PlayerStayAbility))]
[RequireComponent(typeof(PlayerWallSlideAbility), typeof(PlayerWallClimbAbility), typeof(PlayerWallGrabAbility))]
[RequireComponent(typeof(PlayerLedgeClimbAbility), typeof(PlayerCrouchAbility))]

public class Player : MonoBehaviour
{

}
