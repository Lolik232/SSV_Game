using UnityEngine;

namespace Spells.Actions.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Spells/Effects/Damage", menuName = "damage")]
    public class DamageActionSO : EffectActionSO
    {
        [SerializeField] private float _value = 0f;
        
        public override EffectAction CreateAction()
        {
            return new DamageAction(this, _value);
        }
    }
}