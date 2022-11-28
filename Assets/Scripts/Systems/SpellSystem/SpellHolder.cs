using System.Collections.Generic;

using All.Interfaces;

using UnityEngine;

namespace Systems.SpellSystem.SpellEffect
{
    [RequireComponent(typeof(ISpellEffectActionVisitor))]
    public class SpellHolder : MonoBehaviour, ILogicUpdate
    {
        [SerializeField] private List<Spell>              _spells           = new();
        [SerializeField] private FilterSO                 _spellFilterSO    ;
        [SerializeField] private MutuallyExclusiveTableSO _exclusiveTableSO;

        private List<Spell>               _spellsToRemove = new();
        private List<Spell>               _spellsToActivate = new();
        private ISpellEffectActionVisitor _visitor;

        private void Awake()
        {
            _visitor = GetComponent<ISpellEffectActionVisitor>();
        }

        public void AddSpell(SpellSO spell)
        {
            if (_spellFilterSO != null && _spellFilterSO.InBlackList(spell))
                return;
            var createdSpell = spell.CreateSpell();
            _spells.Add(createdSpell);
            _spellsToActivate.Add(createdSpell);
        }

        public void AddSpells(List<SpellSO> spells)
        {
            foreach (var spell in spells)
            {
                AddSpell(spell);
            }
        }

        private void Update()
        {
            RemoveSpellsFromList();
            ActivateSpells();
            ApplyAllSpells();
            LogicUpdate();
            CheckLiveCycle();
        }

        private void ActivateSpells()
        {
            _spellsToActivate.ForEach(s => s.Start());
        }

        private void RemoveSpellsFromList()
        {
            _spells.RemoveAll(s => _spellsToRemove.Contains(s));

            _spellsToRemove.Clear();
        }

        private void ApplyAllSpells()
        {
            foreach (var spell in _spells)
            {
                spell.ApplyEffects(_visitor);
            }
        }

        private void CheckLiveCycle()
        {
            _spellsToRemove = _spells.FindAll(s => s.IsEnd);
        }

        public void LogicUpdate()
        {
            foreach (var spell in _spells)
            {
                spell.LogicUpdate();
            }
        }
    }
}