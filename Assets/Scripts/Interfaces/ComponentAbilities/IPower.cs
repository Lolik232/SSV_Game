using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public interface IPower
{
    public float MaxMana
    {
        get;
        set;
    }

    public float Mana
    {
        get;
    }

    public bool ManaRegenBlocked
    {
        get;
    }

    public float ManaRegeneration
    {
        get;
    }

    public void UseMana(float cost);

    public void RestoreMana(float regeneration);

    public void BlockManaRegen();

    public void UnlockManaRegen();
}
