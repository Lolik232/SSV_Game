using System;

using All.Interfaces;

using UnityEngine;

namespace Systems.SpellSystem.SpellEffect
{
    [Serializable]
    public class Effect : ICloneable
    {
        [SerializeField] private EffectSO            _effectSO;
        [SerializeField] private EffectApplyStrategy _applyStrategy;
        [SerializeField] private EffectAction        _effectAction;

        public EffectSO EffectSO => _effectSO;
        public EffectActionSO EffectActionSO => _effectAction.EffectActionSO;
        public EffectApplyStrategySO EffectApplyStrategySO => _applyStrategy.ApplyStrategySO;

        public Effect(EffectSO effectSo,
                      EffectApplyStrategy applyStrategy,
                      EffectAction effectAction)
        {
            _effectSO = effectSo;
            _applyStrategy = applyStrategy;
            _effectAction = effectAction;
        }

        public void Apply(ISpellEffectActionVisitor visitor)
        {
            if (!_applyStrategy.CanApply())
                return;

            _effectAction.Apply(visitor);
        }

        public object Clone()
        {
            return new Effect(_effectSO,
                              (EffectApplyStrategy)_applyStrategy.Clone(),
                              _effectAction);
        }
    }
}