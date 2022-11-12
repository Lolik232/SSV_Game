using UnityEngine;

namespace Systems.SpellSystem.SpellEffect.Actions.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Spell/Effect/Action/Damage")]
    public class DamageActionSO : EffectActionSO
    {
        [SerializeField] private float _value = 0f;
        
        public override EffectAction CreateAction()
        {
            return new DamageAction(this, _value);
        }
    }
}