using System;
using System.Collections.Generic;

using All.Interfaces;

using Systems.SpellSystem.SpellEffect.SpellLiveCycle;

using UnityEngine;

namespace Systems.SpellSystem.SpellEffect
{
    [Serializable]
    public class Spell : ILogicUpdate
    {
        public SpellSO SpellSO
        {
            get; private set;
        }

        [SerializeField] private List<Effect> _effects = new();

        private BaseLiveCycle _liveCycle;

        public bool IsEnd => _liveCycle.IsEnd();


        public Spell(SpellSO spellSO, BaseLiveCycle liveCycle, params Effect[] effects)
        {
            SpellSO = spellSO;
            _liveCycle = liveCycle;
            _effects.AddRange(effects);
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

        public void Start()
        {
            _liveCycle.Start();
        }
    }
}