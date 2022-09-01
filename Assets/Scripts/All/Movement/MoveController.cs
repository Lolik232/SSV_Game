using System;

using UnityEngine;

public abstract class MoveController
{
    private Unit Unit { get; set; }
    private UnitData Data { get; set; }
    public ValueChangingAction<int> FacingDirection { get; private set; }

    public Vector2 CurrentVelocity { get; private set; }

    protected Vector2 HoldPosition;
    protected Vector2 HoldVelocity;

    protected bool NeedToHoldPosition;
    protected bool NeedToHoldVelocity;

    public MoveController(Unit unit, UnitData data)
    {
        Unit = unit;
        Data = data;
        FacingDirection = new ValueChangingAction<int>();
    }

    public virtual void Initialize()
    {
        FacingDirection.Value = 1;
    }

    public virtual void LogicUpdate()
    {
        CheckIfHoldPosition();
        CheckIfHoldVelocity();
        CurrentVelocity = Unit.RB.velocity;
    }
    public virtual void PhysicsUpdate()
    {

    }

    public void CheckIfShouldFlip(int direction)
    {
        if (direction != 0 && direction != FacingDirection)
        {
            Flip();
        }
    }

    protected void CheckIfHoldPosition()
    {
        if (NeedToHoldPosition)
        {
            Unit.transform.position = HoldPosition;
            SetVelocityZero();
        }
    }

    protected void CheckIfHoldVelocity()
    {
        if (NeedToHoldVelocity)
        {
            SetVelocity(HoldVelocity);
        }
    }

    protected void MoveToPosition(Vector2 position) => Unit.transform.position = position;

    protected void SetVelocityZero() => SetVelocity(Vector2.zero);
    protected void SetVelocity(float velocity, Vector2 angle, int direction) => SetVelocity(angle.normalized.x * velocity * direction, angle.normalized.y * velocity);
    protected void SetVelocity(float velocity, Vector2 angle) => SetVelocity(velocity * angle);
    protected void SetVelocity(float velocityX, float velocityY) => SetVelocity(new Vector2(velocityX, velocityY));
    protected void SetVelocityX(float velocity) => SetVelocity(velocity, Unit.RB.velocity.y);
    protected void SetVelocityY(float velocity) => SetVelocity(Unit.RB.velocity.x, velocity);

    private void SetVelocity(Vector2 velocity)
    {
        Unit.RB.velocity = velocity;
        CurrentVelocity = velocity;
    }

    protected virtual void Flip() => Flip(Unit.transform);
    private void Flip(Transform targetTransform)
    {
        FacingDirection.Value = -FacingDirection;
        targetTransform.Rotate(0f, 180f, 0f);
    }
}
