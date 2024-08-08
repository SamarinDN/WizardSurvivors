using UniRx;

namespace DataHolders
{
	public sealed class HealthPointsDataHolder
	{
		public readonly ReactiveProperty<float> CurrentHealth = new();
		public readonly ReactiveProperty<float> MaxHealth = new();
	}
}