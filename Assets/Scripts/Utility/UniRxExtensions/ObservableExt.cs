using System;
using UniRx;
using UnityEngine;

namespace Utility.UniRxExtensions
{
	public static class ObservableExt
	{
		public static IObservable<float> EveryUpdateCountdown(float initialTime)
		{
			return Observable.EveryUpdate()
				.Scan(initialTime, (countdown, _) => countdown - Time.deltaTime)
				.TakeWhile(timer => timer > 0f);
		}
	}
}