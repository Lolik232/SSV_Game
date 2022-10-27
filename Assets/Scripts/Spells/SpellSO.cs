using System;
using System.Collections.Generic;
using All.Interfaces;
using UnityEngine;

namespace Spells
{
    [CreateAssetMenu(menuName = "Spell/Spell", fileName = "spell")]
    public class SpellSO : ScriptableObject
    {
        // TODO: add spell class
        [SerializeField] private int        _baseSpell;
        [SerializeField] private ILiveCycle _liveCycle;
        // TODO: add spell effect
        [SerializeField] private List<Effect> _effects = new();
    }
}