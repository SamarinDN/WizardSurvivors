using UnityEngine;

namespace Definitions.Spells
{
	public abstract class SpellDefinition : ScriptableObject, ISpellDefinition
	{
		[Header("Spell base stats")]
		[SerializeField, Min(0f)]
		private float spellSecondsCooldown;

		[Header("Spell icon")]
		[SerializeField]
		private Sprite spellIcon;

		[Header("Spell view")]
		[SerializeField]
		private GameObject spellView;

		public float SpellSecondsCooldown => spellSecondsCooldown;
		public Sprite SpellIcon => spellIcon;
		public GameObject SpellView => spellView;
	}
}