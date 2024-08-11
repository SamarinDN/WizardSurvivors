using UniRx;

namespace Services.CastSpellService
{
	public interface ICastSpellService
	{
		IReadOnlyReactiveProperty<bool> IsSpellCanBeCast { get; }
		IReadOnlyReactiveProperty<float> SpellCooldownPercentage { get; }
	}
}