using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spells.Actions
{
    [Serializable]
    public class BlockAbility : EffectAction
    {
        [SerializeField] private List<AbilitySO> _abilitiesToBlock = new List<AbilitySO>();
        
        
        public override void Apply()
        {
            
        }

        public BlockAbility(EffectActionSO effectActionSo) : base(effectActionSo)
        {
        }
    }
}