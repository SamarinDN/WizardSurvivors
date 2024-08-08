using System;
using DataHolders;
using Definitions.Enemies;
using JetBrains.Annotations;
using UniRx;
using Zenject;

namespace Handlers.Enemy
{
	[UsedImplicitly]
	public sealed class EnemyHealthHandler : IPoolable, IDisposable
	{
		private readonly IDisposable _receivedDamageSubscription;

		private readonly ReceivedDamageDataHolder _receivedDamageDataHolder;
		private readonly HealthPointsDataHolder _healthPointsDataHolder;
		private readonly IBaseGroundMovingUnitDefinition _baseGroundMovingUnitDefinition;

		public EnemyHealthHandler(
			ReceivedDamageDataHolder receivedDamageDataHolder,
			HealthPointsDataHolder healthPointsDataHolder,
			IBaseGroundMovingUnitDefinition baseGroundMovingUnitDefinition)
		{
			_healthPointsDataHolder = healthPointsDataHolder;
			_baseGroundMovingUnitDefinition = baseGroundMovingUnitDefinition;
			_receivedDamageSubscription = receivedDamageDataHolder.Damage
				.Subscribe(TakeDamage);
		}

		public void TakeDamage(float damage)
		{
			_healthPointsDataHolder.CurrentHealth.Value -= damage;
		}

		public void OnSpawned()
		{
			var hp = _baseGroundMovingUnitDefinition.HealthPoints;
			_healthPointsDataHolder.MaxHealth.Value = hp;
			_healthPointsDataHolder.CurrentHealth.Value = hp;
		}

		public void OnDespawned()
		{
		}

		public void Dispose()
		{
			_receivedDamageSubscription?.Dispose();
		}
	}
}