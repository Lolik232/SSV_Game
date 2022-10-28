using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Spells
{
    [Serializable]
    public class Periodic : EffectApplyStrategy
    {
        private float _lastUsedTime;
        private float _period;
        private bool  _started = false;

        public Periodic(float period)
        {
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