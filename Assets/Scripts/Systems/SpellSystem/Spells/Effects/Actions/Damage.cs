using System;
using All.Interfaces;
using UnityEngine;

namespace Spells.Actions
{
    [Serializable]
    public class Damage : EffectAction
    {
        [SerializeField] private float _value;

        public float Value => _value;

        public Damage(EffectActionSO effectActionSO) : base(effectActionSO) { }

        public Damage(EffectActionSO effectActionSO, float value) : base(effectActionSO)
        {
            _value = value;
        }

        public override void Apply(ISpellEffectActionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}