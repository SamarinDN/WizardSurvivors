using UniRx;
using Utility.UniRxExtensions;

namespace Services.InputService
{
	public interface IPlayerInputService
	{
		public IReadOnlyReactiveProperty<float> MoveDirection { get; }
		public IReadOnlyReactiveProperty<float> RotateDirection { get; }
		public ReadOnlyReactiveCommand CastSpell { get; }
		public ReadOnlyReactiveCommand SelectPreviousSpell { get; }
		public ReadOnlyReactiveCommand SelectNextSpell { get; }
	}
}