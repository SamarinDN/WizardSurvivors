using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Definitions.Spells
{
	[CreateAssetMenu(fileName = "SpellBook", menuName = "Definitions/SpellBook")]
	public sealed class SpellBook : ScriptableObject
	{
		[SerializeField]
		private List<SpellDefinition> spells;

		public ReadOnlyCollection<SpellDefinition> Spells => spells.AsReadOnly();
	}
}