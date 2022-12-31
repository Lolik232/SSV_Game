using System.Collections;
using System.Collections.Generic;
using All.Interfaces;
using Systems.SpellSystem.SpellEffect.Actions;
using UnityEngine;

namespace TestComponents
{
    public class SkeletonEffectApplyVisitor : MonoBehaviour, ISpellEffectActionApplier
    {
        private SkeletonWarriorBehaviour _skeletonBehaviour;
        private List<IBlockableBySpell>  _blockedComponents = new();

        private void Awake()
        {
            _skeletonBehaviour = GetComponent<SkeletonWarriorBehaviour>();
            GetComponents(_blockedComponents);
        }

        public void Visit(DamageAction damageAction)
        {
            throw new System.NotImplementedException();
        }

        public void Visit(BlockAbilityAction blockAbilityAction)
        {
            foreach (var blockedAbility in blockAbilityAction.AbilitiesToBlock)
            {
                foreach (var blockedComponent in _blockedComponents)
                {
                    if (blockedComponent.Description == blockedAbility)
                    {
                        StartCoroutine(UnlockTimeOut(blockedComponent));
                    }
                }
            }
        }

        private IEnumerator UnlockTimeOut(IBlockableBySpell component)
        {
            component.Block();

            yield return new WaitForSeconds(0.5f);

            if (_skeletonBehaviour != null && _skeletonBehaviour.enabled)
            {
                component.Unlock();
            }
        }
    }
}