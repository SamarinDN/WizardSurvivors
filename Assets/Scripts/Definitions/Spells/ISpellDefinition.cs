using UnityEngine;

namespace Definitions.Spells
{
	public interface ISpellDefinition
	{
		public float SpellSecondsCooldown { get; }
		public Sprite SpellIcon { get; }
		public GameObject SpellView { get; }
	}
}