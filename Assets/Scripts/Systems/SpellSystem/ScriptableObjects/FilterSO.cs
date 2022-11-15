using System.Collections.Generic;

using UnityEngine;

namespace Systems.SpellSystem.SpellEffect
{
    [CreateAssetMenu(fileName = "Filter", menuName = "Spell/Filter")]
    public class FilterSO : ScriptableObject
    {
        private List<SpellSO>                _spellBlackList = new();
        public IReadOnlyCollection<SpellSO> SpellBlackList => _spellBlackList.AsReadOnly();


        private List<SpellSO>                _spellWhiteList = new();
        public IReadOnlyCollection<SpellSO> SpellWhiteList => _spellWhiteList.AsReadOnly();

        public bool InBlackList(SpellSO spell)
        {
            return !InWhiteList(spell);
        }

        public bool InWhiteList(SpellSO spell)
        {
            if (_spellBlackList.Contains(spell))
                return false;

            return _spellWhiteList.Count == 0 || _spellWhiteList.Contains(spell);
        }
    }
}