using System;
using System.Collections.Generic;
using All.Interfaces;
using UnityEngine;

namespace Spells.Actions
{
    [Serializable]
    public class BlockAbilityAction : EffectAction
    {
        [SerializeField] private List<AbilitySO> _abilitiesToBlock = new List<AbilitySO>();

        public BlockAbilityAction(EffectActionSO effectActionSo) : base(effectActionSo) { }

        public BlockAbilityAction(EffectActionSO effectActionSo, IEnumerable<AbilitySO> abilitiesToBlock) : base(effectActionSo)
        {
            _abilitiesToBlock = new List<AbilitySO>(abilitiesToBlock);
        }

        public override void Apply(ISpellEffectActionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}