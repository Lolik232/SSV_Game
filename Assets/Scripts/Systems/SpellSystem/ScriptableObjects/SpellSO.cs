using System;
using System.Collections.Generic;
using System.Linq;
using All.BaseClasses;
using All.Interfaces;
using Spells.SpellLiveCycle;
using Systems.SpellSystem.Spells;
using Unity.VisualScripting;
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

        public Spell CreateSpell()
        {
            return new Spell(this, _liveCycle.CreateLiveCycle(), _effects.ToArray());
        }
    }
}