using UnityEngine;

namespace Definitions.Spells
{
	public abstract class SpellDefinition : ScriptableObject
	{
		[Header("Spell base stats")]
		[SerializeField, Min(0f)]
		private float spellSecondsCooldown;

		public float SpellSecondsCooldown => spellSecondsCooldown;
	}
}