using System;

using All.Interfaces;

using UnityEngine;

namespace Systems.SpellSystem.SpellEffect
{
    [Serializable]
    public abstract class EffectAction
    {
        [SerializeField] private EffectActionSO _effectActionSO;
        public EffectActionSO EffectActionSO => _effectActionSO;

        protected EffectAction(EffectActionSO effectActionSo)
        {
            _effectActionSO = effectActionSo;
        }

        public abstract void Apply(ISpellEffectActionVisitor visitor);
    }
}