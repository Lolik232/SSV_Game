using System;
using All.Interfaces;

namespace Spells
{
    public abstract class EffectApplyStrategy : ICloneable
    {
        private EffectApplyStrategySO _applyStrategySO;
        public  EffectApplyStrategySO ApplyStrategySO => _applyStrategySO;


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