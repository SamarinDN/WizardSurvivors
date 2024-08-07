using JetBrains.Annotations;
using UniRx;

namespace Services.CastSpellService.SpellContainer
{
	[UsedImplicitly]
	public sealed class SpellActivityStateHolder
	{
		public readonly ReactiveProperty<bool> IsSpellActive = new();
	}
}