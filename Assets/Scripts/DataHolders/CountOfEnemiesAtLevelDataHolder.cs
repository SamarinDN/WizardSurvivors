using JetBrains.Annotations;
using UniRx;

namespace DataHolders
{
	[UsedImplicitly]
	public sealed class CountOfEnemiesAtLevelDataHolder
	{
		public readonly ReactiveProperty<int> CountOfEnemies = new();
	}
}