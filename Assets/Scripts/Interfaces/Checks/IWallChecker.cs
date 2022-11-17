using UnityEngine;

public interface ITouchingWall
{
    public bool TouchingWall
    {
        get;
    }
    public bool TouchingWallBack
    {
        get;
    }
    public Vector2 WallPosition
    {
        get;
    }
    public int WallDirection
    {
        get;
    }
    public float YOffset
    {
        get;
    }
}
