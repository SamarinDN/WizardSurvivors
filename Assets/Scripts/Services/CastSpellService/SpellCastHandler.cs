using Definitions.Spells;
using JetBrains.Annotations;
using UnityEngine;

namespace Services.CastSpellService
{
	[UsedImplicitly]
	internal sealed class SpellCastHandler
	{
		public void CastSpell(SpellDefinition spellDefinition, Vector3 casterPosition)
		{
			Debug.Log($"Try cast spell {spellDefinition.name} at position {casterPosition}");
		}
	}
}