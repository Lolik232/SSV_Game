using UnityEngine;

public interface IRotateable
{
    public int FacingDirection
    {
        get;
    }
    public int BodyDirection
    {
        get;
    }
    public bool IsRotationLocked
    {
        get;
    }

    public void RotateIntoDirection(int direction);

    public void RotateBodyIntoDirection(int direction);

    public void LookAt(Vector2 position);

    public void RotateBodyAt(Vector2 position);

    public void BlockRotation();

    public void UnlockRotation();
}
