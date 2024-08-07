using System;
using Definitions.Enemies;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Services.EnemySpawnService
{
	internal sealed class EnemyGameObjectPoolableFacade : MonoBehaviour,
		IPoolable<Vector3, Quaternion, IMemoryPool>, IDisposable
	{
		private PoolableManager _poolableManager;
		private IMemoryPool _pool;

		[Inject]
		private void Constructor(PoolableManager poolableManager)
		{
			_poolableManager = poolableManager;
		}

		public void OnSpawned(Vector3 spawnEnemyPosition, Quaternion spawnEnemyDirection, IMemoryPool pool)
		{
			_pool = pool;
			var enemyTransform = transform;
			enemyTransform.position = spawnEnemyPosition;
			enemyTransform.rotation = spawnEnemyDirection;
			_poolableManager.TriggerOnSpawned();
		}

		public void OnDespawned()
		{
			_pool = null;
			_poolableManager.TriggerOnDespawned();
		}

		public void Dispose()
		{
			DespawnFromPool();
		}

		private void DespawnFromPool()
		{
			_pool?.Despawn(this);
		}

		[UsedImplicitly]
		public class Factory : PlaceholderFactory<Vector3, Quaternion, EnemyGameObjectPoolableFacade>
		{
			public UnitDefinition UnitType { get; private set; }

			[Inject]
			private void Constructor(UnitDefinition unitDefinition)
			{
				UnitType = unitDefinition;
			}
		}
	}
}