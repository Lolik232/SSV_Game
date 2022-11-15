using System;

using All.Interfaces;

using UnityEngine;

namespace Systems.SpellSystem.SpellEffect.Actions
{
    [Serializable]
    public class DamageAction : EffectAction
    {
        [SerializeField] private float _value;

        public float Value => _value;

        public DamageAction(EffectActionSO effectActionSO) : base(effectActionSO) { }

        public DamageAction(EffectActionSO effectActionSO, float value) : base(effectActionSO)
        {
            _value = value;
        }

        public override void Apply(ISpellEffectActionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}