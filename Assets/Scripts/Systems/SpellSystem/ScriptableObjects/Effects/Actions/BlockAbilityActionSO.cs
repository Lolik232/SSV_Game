using System.Collections.Generic;

using UnityEngine;

namespace Systems.SpellSystem.SpellEffect.Actions.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Spell/Effect/Action/Block Ability")]
    public class BlockAbilityActionSO : EffectActionSO
    {
        [SerializeField] private List<AbilitySO> _abilitiesToBlock = new();

        public override EffectAction CreateAction()
        {
            return new BlockAbilityAction(this);
        }
    }
}