using System;

using UnityEngine;

namespace Systems.SpellSystem.SpellEffect
{
    [Serializable]
    public class PeriodicApplyStrategy : EffectApplyStrategy
    {
        [SerializeField] private float _lastUsedTime = 0f;
        [SerializeField] private float _period       = 0f;
        [SerializeField] private bool  _started      = false;

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

        public override object Clone()
        {
            return new PeriodicApplyStrategy(ApplyStrategySO, _period);
        }
    }
}