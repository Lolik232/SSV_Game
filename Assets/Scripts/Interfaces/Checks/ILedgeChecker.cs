using UnityEngine;

public interface ITouchingLedge {
    public bool TouchingLegde
    {
        get;
    }
    public bool TouchingGround
    {
        get;
    }
    public Vector2 GroundPosition
    {
        get;
    }
    public float YOffset
    {
        get;
    }
}
