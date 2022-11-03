using System;
using System.Collections.Generic;
using System.Linq;
using All.Interfaces;
using Spells;
using Systems.SpellSystem.Spells;
using UnityEngine;

namespace Systems.SpellSystem
{
    public class SpellHolder : MonoBehaviour, ILogicUpdate
    {
        [SerializeField] private List<Spell>               _spells         = new();
        [SerializeField] private FilterSO                  _spellFilter    = default;
        private                  List<Spell>               _spellsToRemove = new();
        private                  ISpellEffectActionVisitor _visitor;

        public void AddSpell(SpellSO spell)
        {
            if (_spellFilter.InBlackList(spell)) return;

            _spells.Add(spell.CreateSpell());
        }

        private void Update()
        {
            RemoveSpellsFromList();
            ApplyAllSpells();
            LogicUpdate();
            CheckLiveCycle();
        }

        private void RemoveSpellsFromList()
        {
            _spells.RemoveAll(s => _spellsToRemove.Contains(s));

            _spellsToRemove.Clear();
        }

        private void ApplyAllSpells()
        {
            
        }

        private void CheckLiveCycle()
        {
            _spellsToRemove = _spells.FindAll(s => s.IsEnd);
        }


        // [SerializeField] private 
        public void LogicUpdate()
        {
            foreach (var spell in _spells)
            {
                spell.LogicUpdate();
            }
        }
    }
}