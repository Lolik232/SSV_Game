using System;
using System.Collections.Generic;
using All.BaseClasses;
using All.Interfaces;
using Spells.SpellLiveCycle;
using UnityEngine;

namespace Spells
{
    [CreateAssetMenu(menuName = "Spell/Spell", fileName = "spell")]
    public class SpellSO : BaseDescriptionSO, IObject
    {
        [SerializeField] private string _name = "spell";

        [SerializeField] private LiveCycleSO _liveCycle;

        [SerializeField] private List<Effect> _effects = new();

        public void AddEffect(EffectSO effect)
        {
            _effects.Add(effect.CreateEffect());
        }

        public string Name => _name;
    }
}