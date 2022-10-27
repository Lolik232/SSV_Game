using System;
using UnityEngine;

namespace Spells.Actions
{
    [Serializable]
    public class Damage : EffectAction
    {
        [SerializeField] private float _value;

        public float Value => _value;
    

        public Damage(EffectActionSO effectActionSO) : base(effectActionSO)
        {
        }

        public override void Apply()
        {
            
        }
    }
}