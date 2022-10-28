using System;

namespace Spells
{
    [Serializable]
    public class OneTime : EffectApplyStrategy
    {
        private bool _effectUsed = false;

        public override void OnApply()
        {
            _effectUsed = true;
        }

        public override bool CanApply() => !_effectUsed;

        // public override void LogicUpdate() { }
    }
}