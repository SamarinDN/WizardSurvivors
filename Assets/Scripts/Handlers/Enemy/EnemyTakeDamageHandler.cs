using System;
using DataHolders;
using Definitions.Enemies;
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
		private readonly float _damageMitigationMultiplier;

		public EnemyTakeDamageHandler(
			IBaseGroundMovingUnitDefinition baseGroundMovingUnitDefinition,
			ReceivedDamageDataHolder receivedDamageDataHolder,
			HealthPointsDataHolder healthPointsDataHolder)
		{
			_damageMitigationMultiplier = baseGroundMovingUnitDefinition.DamageReductionMultiplier;
			_healthPointsDataHolder = healthPointsDataHolder;
			_receivedDamageSubscription = receivedDamageDataHolder.Damage
				.Subscribe(TakeDamage);
		}

		private void TakeDamage(float damage)
		{
			_healthPointsDataHolder.CurrentHealth.Value -= damage * _damageMitigationMultiplier;
		}

		public void Dispose()
		{
			_receivedDamageSubscription?.Dispose();
		}
	}
}