using DataHolders;
using JetBrains.Annotations;
using Zenject;

namespace Handlers.Enemy
{
	[UsedImplicitly]
	public sealed class EnemyHealthRestoreOnSpawnHandler : IPoolable
	{
		private readonly HealthPointsDataHolder _healthPointsDataHolder;
		private readonly float _baseHealthPoints;

		public EnemyHealthRestoreOnSpawnHandler(
			HealthPointsDataHolder healthPointsDataHolder,
			float baseHealthPoints)
		{
			_healthPointsDataHolder = healthPointsDataHolder;
			_baseHealthPoints = baseHealthPoints;
		}

		public void OnSpawned()
		{
			_healthPointsDataHolder.CurrentHealth.Value = _baseHealthPoints;
			_healthPointsDataHolder.MaxHealth.Value = _baseHealthPoints;
		}

		public void OnDespawned()
		{
		}
	}
}