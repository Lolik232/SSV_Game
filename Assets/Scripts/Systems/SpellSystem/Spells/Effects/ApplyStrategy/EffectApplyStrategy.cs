using System;
using All.Interfaces;
using UnityEngine;

namespace Systems.SpellSystem.SpellEffect
{
    [Serializable]
    public abstract class EffectApplyStrategy : ICloneable
    {
        [SerializeField] private EffectApplyStrategySO _applyStrategySO;
        public                   EffectApplyStrategySO ApplyStrategySO => _applyStrategySO;


        protected EffectApplyStrategy(EffectApplyStrategySO applyStrategySo)
        {
            _applyStrategySO = applyStrategySo;
        }

        public abstract void OnApply();
        public abstract bool CanApply();

        // public abstract void LogicUpdate();
        public abstract object Clone();
    }
}