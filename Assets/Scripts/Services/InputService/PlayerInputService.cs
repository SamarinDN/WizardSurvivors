using JetBrains.Annotations;
using UniRx;
using Utility.UniRxExtensions;

namespace Services.InputService
{
	[UsedImplicitly]
	internal sealed class PlayerInputService
		: IPlayerInputPropertyHolder, IPlayerInputService
	{
		public ReactiveProperty<float> MoveDirectionInternal { get; } = new();
		public ReactiveProperty<float> RotateDirectionInternal { get; } = new();
		public ReactiveCommand CastSpellInternal { get; } = new();
		public ReactiveCommand SelectPreviousSpellInternal { get; } = new();
		public ReactiveCommand SelectNextSpellInternal { get; } = new();

		public IReadOnlyReactiveProperty<float> MoveDirection => MoveDirectionInternal;
		public IReadOnlyReactiveProperty<float> RotateDirection => RotateDirectionInternal;
		public ReadOnlyReactiveCommand CastSpell { get; }
		public ReadOnlyReactiveCommand SelectPreviousSpell { get; }
		public ReadOnlyReactiveCommand SelectNextSpell { get; }

		public PlayerInputService()
		{
			CastSpell = new ReadOnlyReactiveCommand(CastSpellInternal);
			SelectPreviousSpell = new ReadOnlyReactiveCommand(SelectPreviousSpellInternal);
			SelectNextSpell = new ReadOnlyReactiveCommand(SelectNextSpellInternal);
		}
	}
}