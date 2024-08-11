using Definitions.Spells;
using UniRx;

namespace Services.SelectSpellService
{
	public interface ISelectSpellService
	{
		IReadOnlyReactiveCollection<SpellDefinition> AvailableSpells { get; }
	}
}