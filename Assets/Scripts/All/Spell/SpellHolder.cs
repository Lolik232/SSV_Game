using System;
using System.Collections.Generic;
using System.Linq;

namespace All.Spell
{
    public class SpellHolder
    {
        public event Action<Spell> SpellAdded;
        public event Action<Spell> SpellRemoved;

        private readonly List<Spell> _spells = new List<Spell>();

        public void AddSpell(Spell spell)
        {
            var foundedSpell = _spells.FirstOrDefault(s => Equals(spell, s));
            if (foundedSpell != null)
            {
                foundedSpell.LiveCycle.Reset();
                return;
            }

            _spells.Add(spell);
            OnSpellAdded(spell);
        }

        public void RemoveSpell(Spell spell)
        {
            var foundedSpell = _spells.FirstOrDefault(s => Equals(s, spell));
            if (foundedSpell == null) return;

            _spells.Remove(spell);
            OnSpellRemoved(spell);
        }

        public void LogicUpdate()
        {
            // упростить?
            // Вариант 1: цикл for однопроходным удалением
            // Варианте 2: ????
            // Производительность???? работет и х*й с ним

            List<Spell> spellsWithEndedLiveCycle = new List<Spell>();

            // работет ли AsParallel?
            _spells.AsParallel().ForAll(s =>
            {
                s.LogicUpdate();
                if (s.LiveCycle.IsEnd())
                {
                    spellsWithEndedLiveCycle.Add(s);
                }
            });

            _spells.RemoveAll(s => spellsWithEndedLiveCycle.Contains(s));
        }

        protected virtual void OnSpellAdded(Spell obj)
        {
            SpellAdded?.Invoke(obj);
        }

        protected virtual void OnSpellRemoved(Spell obj)
        {
            SpellRemoved?.Invoke(obj);
        }
    }
}