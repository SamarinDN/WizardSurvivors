using UnityEngine;

namespace Definitions.Spells
{
	public abstract class SpellDefinition : ScriptableObject
	{
		[Header("Spell base stats")]
		[SerializeField, Min(0f)]
		private float spellSecondsCooldown;

		[Header("Spell icon")]
		[SerializeField]
		private Sprite spellIcon;

		public float SpellSecondsCooldown => spellSecondsCooldown;
		public Sprite SpellIcon => spellIcon;
	}
}