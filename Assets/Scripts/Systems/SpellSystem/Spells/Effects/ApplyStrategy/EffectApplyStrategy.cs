using System;

using UnityEngine;

namespace Systems.SpellSystem.SpellEffect
{
    [Serializable]
    public class EffectApplyStrategy : ICloneable
    {
        [SerializeField] private EffectApplyStrategySO _applyStrategySO;
        public EffectApplyStrategySO ApplyStrategySO => _applyStrategySO;


        protected EffectApplyStrategy(EffectApplyStrategySO applyStrategySo)
        {
            _applyStrategySO = applyStrategySo;
        }

        public virtual void OnApply()  { }
        public virtual bool CanApply() { return true;}

        // public abstract void LogicUpdate();
        public virtual object Clone()
        {
            return new EffectApplyStrategy(_applyStrategySO);
        }
    }
}