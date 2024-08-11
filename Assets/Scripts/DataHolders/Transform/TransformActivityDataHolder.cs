using JetBrains.Annotations;
using UniRx;

namespace DataHolders.Transform
{
	[UsedImplicitly]
	public sealed class TransformActivityDataHolder
	{
		public readonly ReactiveProperty<bool> IsActive = new();
	}
}