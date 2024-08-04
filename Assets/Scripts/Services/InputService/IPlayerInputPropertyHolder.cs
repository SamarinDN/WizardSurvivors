using UniRx;

namespace Services.InputService
{
	internal interface IPlayerInputPropertyHolder
	{
		public ReactiveProperty<float> MoveDirectionInternal { get; }
		public ReactiveProperty<float> RotateDirectionInternal { get; }
		public ReactiveCommand CastSpellInternal { get; }
		public ReactiveCommand SelectPreviousSpellInternal { get; }
		public ReactiveCommand SelectNextSpellInternal { get; }
	}
}