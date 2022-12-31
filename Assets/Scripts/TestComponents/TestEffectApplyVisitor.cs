using All.Interfaces;

using Systems.SpellSystem.SpellEffect.Actions;

using UnityEngine;

namespace TestComponents
{
    public class TestEffectApplyVisitor : MonoBehaviour, ISpellEffectActionApplier
    {
        public void Visit(DamageAction damageAction)
        {
            Debug.Log($"EFFECT ACTION DAMAGE: {damageAction.Value}");
        }

        public void Visit(BlockAbilityAction blockAbilityAction)
        {
            Debug.Log($"EFFECT ACTION BLOCK AB {blockAbilityAction.AbilitiesToBlock[0].name}");
        }
    }
}