using Definitions.Enemies;
using JetBrains.Annotations;
using UnityEngine;

namespace Services.EnemySpawnService
{
	[UsedImplicitly]
	internal sealed class EnemySpawnHandler
	{
		private readonly EnemyGameObjectPoolableFacade.Factory[] _factories;

		public EnemySpawnHandler(EnemyGameObjectPoolableFacade.Factory[] factories)
		{
			_factories = factories;
		}

		public void SpawnEnemy(UnitDefinition unitDefinition,
			Vector3 spawnEnemyPosition, Quaternion spawnEnemyDirection)
		{
			foreach (var factory in _factories)
			{
				if (factory.UnitType != unitDefinition)
				{
					continue;
				}

				factory.Create(spawnEnemyPosition, spawnEnemyDirection);
				return;
			}
		}
	}
}