using System.Collections.ObjectModel;

namespace Definitions.Spells
{
	public interface ISpellBook
	{
		public ReadOnlyCollection<SpellDefinition> Spells { get; }
	}
}