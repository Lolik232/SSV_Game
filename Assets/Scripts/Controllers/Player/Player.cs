using UnityEngine;

[RequireComponent(typeof(GroundedState), typeof(InAirState), typeof(TouchingWallState))]
[RequireComponent(typeof(PlayerInputReader))]
[RequireComponent(typeof(GroundChecker), typeof(CeilChecker), typeof(WallChecker))]
[RequireComponent(typeof(LedgeChecker))]

public class Player : MonoBehaviour
{

}
