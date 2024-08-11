using JetBrains.Annotations;
using UniRx;

namespace DataHolders
{
	[UsedImplicitly]
	public sealed class InvincibilityDataHolder
	{
		public readonly ReactiveProperty<bool> IsInvincibility = new();
		public readonly ReactiveProperty<float> InvincibilityDuration = new();
		public readonly ReactiveProperty<float> InvincibilityTimer = new();
	}
}