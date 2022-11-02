using System;

namespace Spells
{
    [Serializable]
    public class OneTimeApplyStrategy : EffectApplyStrategy
    {
        private bool _effectUsed = false;

        public OneTimeApplyStrategy(EffectApplyStrategySO applyStrategySo) : base(applyStrategySo)
        {
        }

        public override void OnApply()
        {
            _effectUsed = true;
        }

        public override bool CanApply() => !_effectUsed;

        public override object Clone()
        {
            return new OneTimeApplyStrategy(ApplyStrategySO);
        }
    }
}