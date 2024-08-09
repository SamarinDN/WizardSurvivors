using UniRx;

namespace DataHolders
{
	public class CountOfEnemiesAtLevelDataHolder
	{
		public readonly ReactiveProperty<int> CountOfEnemies = new();
	}
}