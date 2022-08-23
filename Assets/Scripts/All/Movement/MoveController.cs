using System;

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public abstract class MoveController : MonoBehaviour
{
    public Int32 FacingDirection { get; private set; }

    public Vector2 CurrentVelocity { get; private set; }

    public Rigidbody2D RB { get; private set; }

    public event Action FlipEvent;

    public void CheckIfShouldFlip(Int32 direction)
    {
        if (direction != 0 && direction != FacingDirection)
        {
            Flip();
        }
    }

    public void SetVelocity(Single velocity, Vector2 angle, Int32 direction) => SetVelocity(angle.normalized.x * velocity * direction, angle.normalized.y * velocity);

    public void SetVelocity(Single velocity, Vector2 angle) => SetVelocity(velocity * angle);

    public void SetVelocityX(Single velocity) => SetVelocity(velocity, CurrentVelocity.y);

    public void SetVelocityY(Single velocity) => SetVelocity(CurrentVelocity.x, velocity);

    private void SetVelocity(Vector2 velocity) => CurrentVelocity = RB.velocity = velocity;

    private void SetVelocity(Single velocityX, Single velocityY) => SetVelocity(new Vector2(velocityX, velocityY));

    protected virtual void Flip() => Flip(transform);

    protected void SendFlip()
    {
        FlipEvent?.Invoke();
    }

    private void Flip(Transform targetTransform)
    {
        FacingDirection = -FacingDirection;
        targetTransform.Rotate(0f, 180f, 0f);

        SendFlip();
    }

    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();

        FacingDirection = 1;
    }

    private void Update()
    {
        CurrentVelocity = RB.velocity;
    }
}
