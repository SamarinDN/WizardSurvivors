using UniRx;

namespace Services.InputService
{
	internal interface IPlayerUIInputPropertyHolder
	{
		public ReactiveCommand SubmitButtonPressedInternal { get; }
	}
}