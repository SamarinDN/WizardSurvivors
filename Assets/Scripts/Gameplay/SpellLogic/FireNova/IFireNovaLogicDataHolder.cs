using UniRx;

namespace Gameplay.SpellLogic.FireNova
{
	public interface IFireNovaLogicDataHolder
	{
		public IReadOnlyReactiveProperty<float> NovaRadius { get; }
	}
}