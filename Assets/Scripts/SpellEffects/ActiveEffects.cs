using System.Collections.Generic;

using All.Interfaces;

using UnityEngine;

namespace All.SpellEffects
{
	// [RequireComponent(ISpellEffectVisitor)]
	public class ActiveEffects : MonoBehaviour, ILogicUpdate
	{
		private List<ISpellEffect> m_effects = new List<ISpellEffect>();
		private ISpellEffectVisitor m_effectApplier;

		private void AddEffect(ISpellEffect effect)
		{
			m_effects.Add(effect);
		}

		private void RemoveEffect(ISpellEffect effect)
		{
			m_effects.Remove(effect);
		}

		private void Awake()
		{
			m_effectApplier = GetComponent<ISpellEffectVisitor>();
		}

		private void OnEnable()
		{
			var spellEffectHolder = GetComponent<SpellEffectHolder>();

			spellEffectHolder.EffectAdded += AddEffect;
			spellEffectHolder.EffectRemoved += RemoveEffect;
		}

		private void OnDisable()
		{
			var spellEffectHolder = GetComponent<SpellEffectHolder>();

			spellEffectHolder.EffectAdded -= AddEffect;
			spellEffectHolder.EffectRemoved -= RemoveEffect;
		}

		public void LogicUpdate()
		{
			m_effects.ForEach(
					e =>
					{
						if (e.CanApply())
						{
							m_effectApplier.Apply(e);
						}
					});
		}
	}
}