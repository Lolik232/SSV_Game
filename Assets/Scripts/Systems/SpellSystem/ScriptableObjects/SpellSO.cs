using System;
using System.Collections.Generic;
using All.BaseClasses;
using All.Interfaces;
using UnityEngine;

namespace Spells
{
    [CreateAssetMenu(menuName = "Spell/Spell", fileName = "spell")]
    public class SpellSO : BaseDescriptionSO, IObject
    {
        [SerializeField] private string _name = "spell";

        [SerializeField] private ILiveCycle _liveCycle;
       
        [SerializeField] private List<EffectSO> _effects = new();
        
        public string Name => _name;
    }
}