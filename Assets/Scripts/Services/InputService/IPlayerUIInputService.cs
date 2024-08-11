using Utility.UniRxExtensions;

namespace Services.InputService
{
	public interface IPlayerUIInputService
	{
		public IReadOnlyReactiveCommand SubmitButtonPressed { get; }
	}
}