using JetBrains.Annotations;
using UniRx;

namespace DataHolders
{
	[UsedImplicitly]
	public sealed class ReceivedDamageDataHolder
	{
		public readonly ReactiveCommand<float> Damage = new();
	}
}