using System;

namespace Systems.SpellSystem.SpellEffect
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
        public override void OnCancel()
        {
        }

        public override bool CanApply()  => !_effectUsed;
        public override bool CanCancel() => _effectUsed;

        public override object Clone()
        {
            return new OneTimeApplyStrategy(ApplyStrategySO);
        }
    }
}