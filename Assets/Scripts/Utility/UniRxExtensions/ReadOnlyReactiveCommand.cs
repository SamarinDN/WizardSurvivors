using System;
using UniRx;

namespace Utility.UniRxExtensions
{
	public sealed class ReadOnlyReactiveCommand : IReadOnlyReactiveCommand
	{
		private readonly ReactiveCommand _reactiveCommand;

		public ReadOnlyReactiveCommand(ReactiveCommand reactiveCommand)
		{
			_reactiveCommand = reactiveCommand;
		}

		public IDisposable Subscribe(IObserver<Unit> observer)
		{
			return _reactiveCommand.Subscribe(observer);
		}
	}
}