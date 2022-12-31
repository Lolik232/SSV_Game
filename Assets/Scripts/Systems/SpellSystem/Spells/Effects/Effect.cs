using System;
using All.Interfaces;
using Systems.SpellSystem.SpellEffect.Actions;
using UnityEngine;

namespace Systems.SpellSystem.SpellEffect
{
    [Serializable]
    public class Effect : ICloneable
    {
        [SerializeField] private EffectSO            _effectSO;
        [SerializeField] private EffectApplyStrategy _applyStrategy;
        [SerializeField] private EffectAction        _effectAction;

        public EffectSO              EffectSO              => _effectSO;
        public EffectActionSO        EffectActionSO        => _effectAction.EffectActionSO;
        public EffectApplyStrategySO EffectApplyStrategySO => _applyStrategy.ApplyStrategySO;

        public Effect(EffectSO            effectSo,
                      EffectApplyStrategy applyStrategy,
                      EffectAction        effectAction)
        {
            _effectSO      = effectSo;
            _applyStrategy = applyStrategy;
            _effectAction  = effectAction;
        }

        public void Apply(ISpellEffectActionVisitor applier)
        {
            if (!_applyStrategy.CanApply()) return;

            _effectAction.Apply(applier);
        }

        public void Cancel(ISpellEffectActionVisitor canceller)
        {
            if (!_applyStrategy.CanCancel()) return;

            _effectAction.Cancel(canceller);
        }

        public object Clone()
        {
            return new Effect(_effectSO,
                              (EffectApplyStrategy)_applyStrategy.Clone(),
                              _effectAction);
        }
    }
}