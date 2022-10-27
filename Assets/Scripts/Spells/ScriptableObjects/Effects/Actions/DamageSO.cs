using UnityEngine;

namespace Spells.Actions.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Spells/Effects/Damage", menuName = "damage")]
    public class DamageSO : EffectActionSO
    {
        public override EffectAction CreateAction()
        {
            return new Damage(this);
        }
    }
}