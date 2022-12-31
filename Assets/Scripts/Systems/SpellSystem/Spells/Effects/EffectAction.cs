using System;

using All.Interfaces;

using UnityEngine;

namespace Systems.SpellSystem.SpellEffect
{
    [Serializable]
    public  class EffectAction
    {
        [SerializeField] private EffectActionSO _effectActionSO;
        public EffectActionSO EffectActionSO => _effectActionSO;

        protected EffectAction(EffectActionSO effectActionSo)
        {
            _effectActionSO = effectActionSo;
        }

        public virtual void Apply(ISpellEffectActionVisitor visitor)
        {
            
        }

        public virtual void Cancel(ISpellEffectActionVisitor canceller)
        {
            
        }
    }
}