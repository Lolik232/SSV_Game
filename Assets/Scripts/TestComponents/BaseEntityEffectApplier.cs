using System;
using System.Collections.Generic;
using All.Interfaces;
using Systems.SpellSystem.SpellEffect.Actions;
using UnityEngine;

namespace TestComponents
{
    [RequireComponent(typeof(Entity))]
    public class BaseEntityEffectApplier : MonoBehaviour, ISpellEffectActionApplier
    {
        private          Entity                  _entity            = default;
        private readonly List<IBlockableBySpell> _blockedComponents = new();

        private void Awake()
        {
            _entity = GetComponent<Entity>();
            GetComponents<IBlockableBySpell>(_blockedComponents);
        }

        public void Visit(DamageAction damageAction)
        {
            
            
            //TODO: add
        }

        public void Visit(BlockAbilityAction blockAbilityAction)
        {
            throw new System.NotImplementedException();
        }
    }
}