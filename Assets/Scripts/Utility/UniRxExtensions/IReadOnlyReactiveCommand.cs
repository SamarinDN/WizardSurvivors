using System;
using UniRx;

namespace Utility.UniRxExtensions
{
	public interface IReadOnlyReactiveCommand : IObservable<Unit>
	{
	}
}