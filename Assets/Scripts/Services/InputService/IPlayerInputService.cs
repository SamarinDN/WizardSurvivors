using UniRx;
using Utility.UniRxExtensions;

namespace Services.InputService
{
	public interface IPlayerInputService
	{
		public IReadOnlyReactiveProperty<float> MoveDirection { get; }
		public IReadOnlyReactiveProperty<float> RotateDirection { get; }
		public IReadOnlyReactiveCommand CastSpell { get; }
		public IReadOnlyReactiveCommand SelectPreviousSpell { get; }
		public IReadOnlyReactiveCommand SelectNextSpell { get; }
	}
}