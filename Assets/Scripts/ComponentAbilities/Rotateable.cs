using UnityEngine;

public class Rotateable : Component, IRotateable
{
    private readonly Blocker _directionBlocker = new();

    public int FacingDirection
    {
        get;
        private set;
    }
    public int BodyDirection
    {
        get;
        private set;
    }
    public bool IsRotationLocked
    {
        get => _directionBlocker.IsLocked;
    }

    public void BlockRotation()
    {
        _directionBlocker.AddBlock();
    }

    public void LookAt(Vector2 position)
    {
        RotateIntoDirection(position.x > transform.position.x ? 1 : -1);
    }

    public void RotateBodyAt(Vector2 position)
    {
        RotateBodyIntoDirection(position.x > transform.position.x ? 1 : -1);
    }

    public void RotateBodyIntoDirection(int direction)
    {
        if (direction != 0)
        {
            BodyDirection = direction;
            switch (direction)
            {
                case 1:
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    break;
                case -1:
                    transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                    break;
                default:
                    break;
            }
        }
    }

    public void RotateIntoDirection(int direction)
    {
        if (direction != 0)
        {
            FacingDirection = direction;
            RotateBodyIntoDirection(direction);
        }
    }

    public void UnlockRotation()
    {
        _directionBlocker.RemoveBlock();
    }
}
