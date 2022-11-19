using UnityEngine;

[RequireComponent(typeof(WallGrabAS), typeof(WallClimbAS), typeof(WallSlideAS))]
[RequireComponent(typeof(Movable), typeof(GrabController))]
[RequireComponent(typeof(MoveController), typeof(Rotateable))]

public class MoveOnWallAbility : Ability
{
    public WallSlideAS Slide
    {
        get;
        private set;
    }
    public WallClimbAS Climb
    {
        get;
        private set;
    }
    public WallGrabAS Grab
    {
        get;
        private set;
    }

    protected override void Awake()
    {
        base.Awake();
        Default = Grab = GetComponent<WallGrabAS>();
        Climb = GetComponent<WallClimbAS>();
        Slide = GetComponent<WallSlideAS>();

        GetAbilityStates<MoveOnWallAbility>();
    }

    protected override void Start()
    {
        base.Start();
        bool StayCondition() => !Entity.IsVelocityLocked && !Entity.IsPositionLocked &&
                                                            (Entity.Behaviour.Grab || Entity.Behaviour.Move.x == Entity.FacingDirection);

        enterConditions.Add(() => StayCondition());
        exitConditions.Add(() => !StayCondition());
    }
}
