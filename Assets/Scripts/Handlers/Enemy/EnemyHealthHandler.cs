using DataHolders;
using Definitions.Enemies;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Handlers.Enemy
{
	[UsedImplicitly]
	public sealed class EnemyHealthHandler : IPoolable, IEnemyHealthHandler
	{
		private readonly HealthPointsDataHolder _healthPointsDataHolder;
		private readonly IBaseGroundMovingUnitDefinition _baseGroundMovingUnitDefinition;

		public EnemyHealthHandler(
			HealthPointsDataHolder healthPointsDataHolder,
			IBaseGroundMovingUnitDefinition baseGroundMovingUnitDefinition)
		{
			_healthPointsDataHolder = healthPointsDataHolder;
			_baseGroundMovingUnitDefinition = baseGroundMovingUnitDefinition;
		}

		public void TakeDamage(float damage)
		{
			_healthPointsDataHolder.CurrentHealth.Value -= damage;
			Debug.Log(
				$"Enemy takeDamage {damage} dmg. " +
				$"Enemy HP = {_healthPointsDataHolder.CurrentHealth.Value} / {_healthPointsDataHolder.MaxHealth.Value}");
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
	}
}