using System;

using UnityEngine;

public abstract class MoveController
{
    public ValueChangingAction<Int32> FacingDirection { get; private set; }

    public ValueChangingAction<Single> CurrentVelocityX { get; private set; }
    public ValueChangingAction<Single> CurrentVelocityY { get; private set; }

    public Rigidbody2D RB { get; private set; }

    public Transform Transform { get; private set; }

    public void CheckIfShouldFlip(Int32 direction)
    {
        if (direction != 0 && direction != FacingDirection)
        {
            Flip();
        }
    }

    public void SetVelocityZero() => SetVelocity(Vector2.zero);
    public void SetVelocity(Single velocity, Vector2 angle, Int32 direction) => SetVelocity(angle.normalized.x * velocity * direction, angle.normalized.y * velocity);
    public void SetVelocity(Single velocity, Vector2 angle) => SetVelocity(velocity * angle);
    public void SetVelocityX(Single velocity) => SetVelocity(velocity, RB.velocity.y);
    public void SetVelocityY(Single velocity) => SetVelocity(RB.velocity.x, velocity);

    private void SetVelocity(Single velocityX, Single velocityY) => SetVelocity(new Vector2(velocityX, velocityY));
    private void SetVelocity(Vector2 velocity)
    {
        RB.velocity = velocity;
        UpdateCurrentVelocity();
    }

    private void UpdateCurrentVelocity()
    {
        CurrentVelocityX.Value = RB.velocity.x;
        CurrentVelocityY.Value = RB.velocity.y;
    }

    protected virtual void Flip() => Flip(Transform);
    private void Flip(Transform targetTransform)
    {
        FacingDirection.Value = -FacingDirection;
        targetTransform.Rotate(0f, 180f, 0f);
    }

    public MoveController()
    {

    }

    public MoveController(Transform transform, Rigidbody2D rigidbody2D)
    {
        CurrentVelocityX = new ValueChangingAction<Single>();
        CurrentVelocityY = new ValueChangingAction<Single>();

        FacingDirection = new ValueChangingAction<Int32>();

        RB = rigidbody2D;
        Transform = transform;
    }

    public virtual void Initialize()
    {
        FacingDirection.Value = 1;
    }

    public virtual void LogicUpdate()
    {
        UpdateCurrentVelocity();
    }
}
