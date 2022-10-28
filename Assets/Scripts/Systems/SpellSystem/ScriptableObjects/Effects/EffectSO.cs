using UnityEngine;

namespace Spells
{
    [CreateAssetMenu(menuName = "Spell/Effect", fileName = "effect")]
    public class EffectSO: ScriptableObject
    {
        [SerializeField] private Effect _baseEffect;
        
        
    }
}