using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Systems.SpellSystem.SpellEffect;
using UnityEngine;
using UnityEngine.Internal;
using Random = UnityEngine.Random;

namespace Systems.SpellSystem.SpellEffect
{
    public class SpellApplier : MonoBehaviour
    {
        [Serializable]
        private struct SpellApplyProbability
        {
            public               SpellSO spellToApply;
            [Range(0,1)]public float   probability;
        }
        
        [SerializeField] private List<SpellApplyProbability> _spellsToApply = new();

        public void Apply(SpellHolder holderToApply)
        {
            foreach (var spellApplyProbability in _spellsToApply)
            {
                if (Random.Range(0f, 1f) >= spellApplyProbability.probability)
                {
                    holderToApply.AddSpell(spellApplyProbability.spellToApply);
                }
            }
        }
    }
}