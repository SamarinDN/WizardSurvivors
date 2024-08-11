using JetBrains.Annotations;
using UniRx;

namespace DataHolders
{
	[UsedImplicitly]
	public sealed class GameStateDataHolder
	{
		public readonly ReactiveCommand GameStart = new();
		public readonly ReactiveCommand GameEnd = new();
	}
}