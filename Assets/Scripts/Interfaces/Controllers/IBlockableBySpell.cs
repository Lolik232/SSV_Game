using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public interface IBlockableBySpell : IBlockable
{
    public AbilitySO Description
    {
        get;
    }
}
