using System.Collections.Generic;
using UnityEngine;

namespace Spells.Actions.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Spells/Effects/Block Ability", menuName = "block ability")]
    public class BlockAbilityActionSO : EffectActionSO
    {
        [SerializeField] private List<AbilitySO> _abilitiesToBlock = new();

        public override EffectAction CreateAction()
        {
            return new BlockAbilityAction(this);
        }
    }
}