using UnityEngine;

[RequireComponent(typeof(PlayerWallGrabAS), typeof(PlayerWallClimbAS), typeof(PlayerWallSlideAS))]
[RequireComponent(typeof(Movable), typeof(GrabController))]
[RequireComponent(typeof(MoveController), typeof(Rotateable))]

public class PlayerMoveVerticalAbility : PlayerAbility
{
    public PlayerWallSlideAS Slide
    {
        get;
        private set;
    }
    public PlayerWallClimbAS Climb
    {
        get;
        private set;
    }
    public PlayerWallGrabAS Grab
    {
        get;
        private set;
    }

    protected override void Awake()
    {
        base.Awake();
        Default = Grab = GetComponent<PlayerWallGrabAS>();
        Climb = GetComponent<PlayerWallClimbAS>();
        Slide = GetComponent<PlayerWallSlideAS>();

        GetAbilityStates<PlayerMoveVerticalAbility>();
    }

    protected override void Start()
    {
        base.Start();
        bool StayCondition() => !Player.IsVelocityLocked && !Player.IsPositionLocked &&
                                                            (Player.Input.Grab || Player.Input.Move.x == Player.FacingDirection);

        enterConditions.Add(() => StayCondition());
        exitConditions.Add(() => !StayCondition());
    }
}
