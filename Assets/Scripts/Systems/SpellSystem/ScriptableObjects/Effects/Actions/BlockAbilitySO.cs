using UnityEngine;

namespace Spells.Actions.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Spells/Effects/Block Ability", menuName = "block ability")]
    public class BlockAbilitySO : EffectActionSO
    {
        public override EffectAction CreateAction()
        {
            return new BlockAbility(this);
        }
    }
}