using UnityEngine;

[RequireComponent(typeof(GroundedState), typeof(InAirState), typeof(TouchingWallState))]
[RequireComponent(typeof(PlayerInputReader))]
[RequireComponent(typeof(GroundChecker), typeof(CeilChecker), typeof(WallChecker))]
[RequireComponent(typeof(LedgeChecker))]
[RequireComponent(typeof(PlayerMoveForwardAbility), typeof(PlayerMoveBackwardAbility), typeof(PlayerStandAbility))]
[RequireComponent(typeof(PlayerWallSlideAbility), typeof(PlayerWallClimbAbility), typeof(PlayerWallGrabAbility))]
[RequireComponent(typeof(PlayerLedgeClimbAbility))]

public class Player : MonoBehaviour
{

}
