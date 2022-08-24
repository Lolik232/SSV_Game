using System;

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public abstract class MoveController : MonoBehaviour
{
    public Int32 FacingDirection { get; private set; }

    public ValueChangingAction<Single> CurrentVelocityX { get; private set; }
    public ValueChangingAction<Single> CurrentVelocityY { get; private set; }

    public Rigidbody2D RB { get; private set; }

    public event Action FlipEvent;

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
    public void SetVelocityX(Single velocity) => SetVelocity(velocity, CurrentVelocityY);
    public void SetVelocityY(Single velocity) => SetVelocity(CurrentVelocityX, velocity);
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

    protected virtual void Flip() => Flip(transform);
    private void Flip(Transform targetTransform)
    {
        FacingDirection = -FacingDirection;
        targetTransform.Rotate(0f, 180f, 0f);

        SendFlip();
    }

    protected void SendFlip()
    {
        FlipEvent?.Invoke();
    }

    protected virtual void Awake()
    {
        CurrentVelocityX = new ValueChangingAction<Single>();
        CurrentVelocityY = new ValueChangingAction<Single>();

        RB = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        FacingDirection = 1;
    }

    protected virtual void Update()
    {
        UpdateCurrentVelocity();
    }
}
