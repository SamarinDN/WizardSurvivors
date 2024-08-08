using System;
using DataHolders;
using JetBrains.Annotations;
using UniRx;

namespace Handlers.Enemy
{
	[UsedImplicitly]
	public sealed class EnemyTakeDamageHandler : IDisposable
	{
		private readonly IDisposable _receivedDamageSubscription;
		private readonly ReceivedDamageDataHolder _receivedDamageDataHolder;
		private readonly HealthPointsDataHolder _healthPointsDataHolder;

		public EnemyTakeDamageHandler(
			ReceivedDamageDataHolder receivedDamageDataHolder,
			HealthPointsDataHolder healthPointsDataHolder)
		{
			_healthPointsDataHolder = healthPointsDataHolder;
			_receivedDamageSubscription = receivedDamageDataHolder.Damage
				.Subscribe(TakeDamage);
		}

		private void TakeDamage(float damage)
		{
			_healthPointsDataHolder.CurrentHealth.Value -= damage;
		}

		public void Dispose()
		{
			_receivedDamageSubscription?.Dispose();
		}
	}
}