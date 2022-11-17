using UnityEngine;

public interface IMoveController
{
    public Vector2Int Move
    {
        get;
        set;
    }
    public Vector2 LookAt
    {
        get;
        set;
    }
}
