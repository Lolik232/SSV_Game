using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

using UnityEngine;

using static UnityEngine.RuleTile.TilingRuleOutput;

public interface ITargetChecker
{
    public int TargetDirection
    {
        get;
    }

    public float TargetDistance
    {
        get;
    }

    public bool TargetDetected
    {
        get;
    }

    public Vector2 TargetPosition
    {
        get;
    }
}
