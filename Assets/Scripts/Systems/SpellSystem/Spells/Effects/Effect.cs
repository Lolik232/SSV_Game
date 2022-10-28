using System;
using All.Interfaces;
using Spells.Actions;
using Spells.Actions.ScriptableObjects;
using Systems.SpellSystem.SpellLiveCycle;
using UnityEngine;

namespace Spells
{
    [Serializable]
    public class Effect
    {
        private EffectSO            _effectSO;
        private EffectApplyStrategy _applyStrategy;
        private EffectAction        _effectAction;
        
        public Effect(EffectSO            effectSo,
                      EffectApplyStrategy applyStrategy,
                      EffectAction        effectAction,
                      BaseLiveCycle       liveCycle)
        {
            _effectSO      = effectSo;
            _applyStrategy = applyStrategy;
            _effectAction  = effectAction;
        }

        public void Apply(ISpellEffectActionVisitor visitor)
        {
            if (!_applyStrategy.CanApply()) return;

            _effectAction.Apply(visitor);
        }
    }
}