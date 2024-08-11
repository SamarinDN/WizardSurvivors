using System;
using DataHolders;
using DataHolders.Transform;
using JetBrains.Annotations;
using UniRx;

namespace Handlers.Enemy
{
	[UsedImplicitly]
	public sealed class EnemyDeathHandler : IDisposable
	{
		private readonly IDisposable _healthSubscription;

		public EnemyDeathHandler(
			HealthPointsDataHolder healthPointsDataHolder,
			TransformActivityDataHolder transformActivityDataHolder)
		{
			_healthSubscription = healthPointsDataHolder.CurrentHealth
				.Where(hp => hp <= 0)
				.Subscribe(_ => transformActivityDataHolder.IsActive.Value = false);
		}

		public void Dispose()
		{
			_healthSubscription.Dispose();
		}
	}
}