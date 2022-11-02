using UnityEngine;

[RequireComponent(typeof(GroundedState), typeof(InAirState), typeof(TouchingWallState))]
[RequireComponent(typeof(PlayerInputReader))]
[RequireComponent(typeof(GroundChecker), typeof(CeilChecker), typeof(WallChecker))]
[RequireComponent(typeof(LedgeChecker))]
[RequireComponent(typeof(PlayerMoveForwardAbility), typeof(PlayerMoveBackwardAbility), typeof(PlayerStandAbility))]
[RequireComponent(typeof(PlayerMoveForwardAbility), typeof(PlayerWallClimbAbility), typeof(PlayerWallGrabAbility))]

public class Player : MonoBehaviour
{

}
