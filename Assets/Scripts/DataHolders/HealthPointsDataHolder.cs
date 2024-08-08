using JetBrains.Annotations;
using UniRx;

namespace DataHolders
{
	[UsedImplicitly]
	public sealed class HealthPointsDataHolder
	{
		public readonly ReactiveProperty<float> CurrentHealth = new();
		public readonly ReactiveProperty<float> MaxHealth = new();
	}
}