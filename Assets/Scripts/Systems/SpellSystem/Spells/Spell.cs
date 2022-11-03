using System;
using System.Collections.Generic;
using System.Linq;
using All.Interfaces;
using Extensions;
using Spells;
using Systems.SpellSystem.SpellLiveCycle;
using UnityEngine;

namespace Systems.SpellSystem.Spells
{
    [Serializable]
    public class Spell : ILogicUpdate
    {
        public SpellSO SpellSO { get; private set; }

        [SerializeField] private List<Effect> _effects = new();

        private BaseLiveCycle _liveCycle;

        public bool IsEnd => _liveCycle.IsEnd();


        public Spell(SpellSO spellSO, BaseLiveCycle liveCycle, params Effect[] effects)
        {
            SpellSO    = spellSO;
            _liveCycle = liveCycle;
            _effects.AddRange(effects.ToList().Clone());
        }

        public void ApplyEffects(ISpellEffectActionVisitor visitor)
        {
            foreach (var effect in _effects)
            {
                effect.Apply(visitor);
            }
        }

        public void LogicUpdate()
        {
            _liveCycle.LogicUpdate();
        }
    }
}