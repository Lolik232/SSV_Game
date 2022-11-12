using Systems.SpellSystem.SpellEffect.SpellLiveCycle;
using UnityEngine;

namespace Systems.SpellSystem.SpellEffect.SpellLiveCycle
{
    [CreateAssetMenu(menuName = "Spell/Live Cycle/Time", fileName = "Time Live Cycle")]

    public class TimeLiveCycleSO : LiveCycleSO
    {
        [SerializeField] private float _time = 0f;
        
        public override BaseLiveCycle CreateLiveCycle()
        {
            return new TimeLiveCycle(_time);
        }
    }
}