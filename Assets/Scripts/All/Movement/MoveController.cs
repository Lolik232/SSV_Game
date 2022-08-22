using System;

using UnityEngine;

public abstract class MoveController : MonoBehaviour
{
    public Int32 FacingDirection { get; private set; }

    public Int32 ControlledMoveX { get; protected set; }
    public Int32 ControlledMoveY { get; protected set; }

    public event Action FlipEvent;

    public void TryFlip(Int32 direction)
    {
        if (IsFlipRequired(direction))
        {
            Flip();
        }
    }

    protected virtual void Flip()
    {
        Flip(transform);
    }

    protected void SendFlip()
    {
        FlipEvent?.Invoke();
    }

    private Boolean IsFlipRequired(Int32 direction)
    {
        return direction != 0 && direction != FacingDirection;
    }

    private void Flip(Transform targetTransform)
    {
        FacingDirection = -FacingDirection;
        targetTransform.Rotate(0f, 180f, 0f);

        SendFlip();
    }

    private void Start()
    {
        FacingDirection = 1;
    }
}
