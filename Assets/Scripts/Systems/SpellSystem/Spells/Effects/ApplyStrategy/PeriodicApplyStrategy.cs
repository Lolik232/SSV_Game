using System;
using UnityEngine;

namespace Systems.SpellSystem.SpellEffect
{
    [Serializable]
    public class PeriodicApplyStrategy : EffectApplyStrategy
    {
        [SerializeField] private float _lastUsedTime = 0f;
        [SerializeField] private float _period       = 0f;
        [SerializeField] private bool  _applied      = false;

        public PeriodicApplyStrategy(EffectApplyStrategySO applyStrategySO,
                                     float                 period)
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
            if (!CanApply()) return;

            _lastUsedTime = Time.time;
            _applied      = true;
        }
        public override void OnCancel()
        {
        }

        
        private bool PeriodIsEnd()
        {
            return Time.time - _lastUsedTime >= _period;
        }

        public override bool CanApply()
        {
            return !_applied && PeriodIsEnd();
        }

        public override bool CanCancel()
        {
            return _applied && PeriodIsEnd();
        }

        public override object Clone()
        {
            return new PeriodicApplyStrategy(ApplyStrategySO, _period);
        }
    }
}