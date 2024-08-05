using UnityEngine;

namespace Definitions.Spells
{
	public abstract class DamagingSpellDefinition : SpellDefinition
	{
		[Header("Damaging spell base stats")]
		[SerializeField, Min(0f)]
		private float spellDamage;

		public float SpellDamage => spellDamage;
	}
}