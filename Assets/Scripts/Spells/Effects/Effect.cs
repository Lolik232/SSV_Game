using System;
using UnityEngine;

namespace Spells
{
    [Serializable]
    public class Effect
    {
        private EffectSO            _effectSO;
        private EffectApplyStrategy _applyStrategy;
        private EffectAction        _effectAction;
        
        

        // TODO: put effect-action visitor!
        public void Apply()
        {
            if (!_applyStrategy.CanApply()) return;
            
            
            
        }
    }
}