using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Serialization;

public class Power : Component, IPower
{
    [SerializeField] private ManaEventChannelSO _didManaChangeEventChannelSo = default;

    [FormerlySerializedAs("_mana")] [SerializeField] private float _startMana;
    [SerializeField] private float _manaRegeneration;
    private float _mana;

    private Blocker _blocker = new();

    public float MaxMana
    {
        get;
        set;
    }
    public float Mana
    {
        get => _mana;
        private set
        {
            _mana = value;
            _didManaChangeEventChannelSo?.RaiseEvent(_mana, MaxMana);
        }
    }

    public bool ManaRegenBlocked
    {
        get => _blocker.IsLocked;
    }
    public float ManaRegeneration
    {
        get;
        private set;
    }

    private void Awake()
    {
        ManaRegeneration = _manaRegeneration;
        Mana = _startMana;
        MaxMana = _startMana;
    }

    public void BlockManaRegen()
    {
        _blocker.AddBlock();
    }

    public void RestoreMana(float regeneration)
    {
        if (regeneration < 0)
        {
            throw new Exception("Regeneration Cannot Be Negative");
        }

        Mana = Mathf.Clamp(Mana + regeneration, 0, MaxMana);
        Debug.Log(this + " Health: " + Mana);
    }

    public void UnlockManaRegen()
    {
        _blocker.RemoveBlock();
    }

    public void UseMana(float cost)
    {
        if (cost < 0)
        {
            throw new Exception("Cost Cannot Be Negative");
        }

        Mana = Mathf.Clamp(Mana - cost, 0, MaxMana);
        Debug.Log(this + " Health: " + Mana);
    }
}
