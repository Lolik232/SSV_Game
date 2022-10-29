using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Spells
{
    [Serializable]
    public class PeriodicApplyStrategy : EffectApplyStrategy
    {
        private float _lastUsedTime = 0f;
        private float _period       = 0f;
        private bool  _started      = false;

        public PeriodicApplyStrategy(EffectApplyStrategySO applyStrategySO, float period)
            : base(applyStrategySO) => SetPeriod(period);

        public PeriodicApplyStrategy(EffectApplyStrategySO applyStrategySO) : base(applyStrategySO) { }
        
        
        public void SetPeriod(float period)
        {
            if (period < 0)
            {
                throw new ArgumentException();
            }

            _period = period;
        }

        public override void OnApply()
        {
            if (CanApply())
            {
                _lastUsedTime = Time.time;
            }
        }

        public override bool CanApply()
        {
            // изначально всегда можно использовать эффект
            return _started == false || Time.time - _lastUsedTime >= _period;
        }
    }
}