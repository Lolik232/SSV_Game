using System.Collections;

using UnityEngine;

public interface IPhysical
{
    public Vector2 Position
    {
        get;
    }

    public Vector2 Velocity
    {
        get;
    }

    public float Gravity
    {
        get;
    }

    public Vector2 Size
    {
        get;
    }

    public Vector2 Offset
    {
        get;
    }

    public Vector2 Center
    {
        get;
    }

    public bool IsPushed
    {
        get;
    }

    public IEnumerator Push(float force, Vector2 angle);
}
