using System;
using System.Collections.Generic;
using System.Linq;

using All.Interfaces;

namespace All.SpellEffects
{
	public class SpellEffectHolder : ILogicUpdate
	{
		public event Action<ISpellEffect> EffectAdded;
		public event Action<ISpellEffect> EffectRemoved;


		public List<KeyValuePair<ISpellEffect, int>> EffectSpells =>
				m_effectSpellsCounts.Select(se =>
						new KeyValuePair<ISpellEffect, int>(se.effect,
								se.SpellsWithEffectCount)).ToList();

		private List<EffectSpellsCount> m_effectSpellsCounts = new List<EffectSpellsCount>();


		private List<ISpellEffect> ActiveEffects => m_effectSpellsCounts.Select(se => se.effect).ToList();

		[Serializable]
		private class EffectSpellsCount
		{
			public bool IsActive => SpellsWithEffectCount > 0;
			public readonly ISpellEffect effect;

			public int SpellsWithEffectCount
			{
				get => m_spellsWithEffectCount;
				private set
				{
					if (value < 0)
						value = 0;
					m_spellsWithEffectCount = value;
				}
			}


			private int m_spellsWithEffectCount = 0;

			public EffectSpellsCount(ISpellEffect effect, int countSpells = 1)
			{
				if (countSpells <= 0)
				{
					throw new ArgumentException("count spells value must be a positive");
				}

				this.effect = effect;
				SpellsWithEffectCount = countSpells;
			}

			public void AddSpell() => AddSpells(1);
			public void RemoveSpell() => RemoveSpells(1);

			public void AddSpells(int count) => SpellsWithEffectCount += count;
			public void RemoveSpells(int count) => SpellsWithEffectCount -= count;
		}


		public void LogicUpdate()
		{
			throw new System.NotImplementedException();
		}

		private void AddEffect(ISpellEffect effect, int spellsCount = 0)
		{
			var effectSpell = m_effectSpellsCounts.FirstOrDefault(se => Equals(se.effect, effect));

			if (effectSpell == null)
			{
				m_effectSpellsCounts.Add(new EffectSpellsCount(effect, spellsCount));
				return;
			}

			effectSpell.AddSpells(spellsCount);
		}

		public void AddEffectsFromSpell(Spell.Spell spell)
		{
			spell.Effects.ToList().ForEach(
					e => AddEffect(e, 1)
			);
		}


		#region event invocators

		protected virtual void OnEffectAdded(ISpellEffect obj)
		{
			EffectAdded?.Invoke(obj);
		}

		protected virtual void OnEffectRemoved(ISpellEffect obj)
		{
			EffectRemoved?.Invoke(obj);
		}

		#endregion
	}
}