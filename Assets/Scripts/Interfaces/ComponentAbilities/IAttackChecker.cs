using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

using UnityEngine;

using static UnityEngine.RuleTile.TilingRuleOutput;

public interface IAttackChecker
{
    public bool AttackPermited
    {
        get;
    }
}
