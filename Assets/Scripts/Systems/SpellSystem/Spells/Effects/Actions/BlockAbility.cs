using System;
using System.Collections.Generic;
using All.Interfaces;
using UnityEngine;

namespace Spells.Actions
{
    [Serializable]
    public class BlockAbility : EffectAction
    {
        [SerializeField] private List<AbilitySO> _abilitiesToBlock = new List<AbilitySO>();

        public BlockAbility(EffectActionSO effectActionSo) : base(effectActionSo) { }

        public override void Apply(ISpellEffectActionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}