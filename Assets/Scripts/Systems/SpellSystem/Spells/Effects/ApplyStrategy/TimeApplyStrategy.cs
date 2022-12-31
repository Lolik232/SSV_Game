using System;
using UnityEngine;

namespace Systems.SpellSystem.SpellEffect
{
    public class TimeApplyStrategy : EffectApplyStrategy
    {
        private float _time = 0f;

        private bool  _used     = false;
        private float _usedTime = 0f;

        public TimeApplyStrategy(EffectApplyStrategySO applyStrategySo, float time) : base(applyStrategySo)
        {
            SetTime(time);
        }

        private void SetTime(float time)
        {
            if (time < 0)
            {
                throw new ArgumentException();
            }

            _time = time;
        }

        public override void OnApply()
        {
            if (!CanApply()) return;

            _usedTime = Time.time;
            _used      = true;
        }
        public override void OnCancel()
        {
        }
        
        private bool TimeIsEnd()
        {
            if (!_used) return false;
            
            return Time.time - _usedTime >= _time;
        }

        public override bool CanApply()
        {
            return !_used && TimeIsEnd();
        }

        public override bool CanCancel()
        {
            return _used && TimeIsEnd();
        }

        public override object Clone()
        {
            return new TimeApplyStrategy(ApplyStrategySO, _time);
        }
    }
}