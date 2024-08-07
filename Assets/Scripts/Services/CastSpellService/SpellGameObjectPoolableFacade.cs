using System;
using DataHolders.Transform;
using Definitions.Spells;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;
using Zenject;

namespace Services.CastSpellService
{
	internal sealed class SpellGameObjectPoolableFacade : MonoBehaviour,
		IPoolable<Vector3, Quaternion, IMemoryPool>, IDisposable
	{
		private PoolableManager _poolableManager;
		private IMemoryPool _pool;

		private TransformActivityDataHolder _transformActivityDataHolder;
		private PositionDataHolder _positionDataHolder;
		private RotationDataHolder _rotationDataHolder;

		[Inject]
		private void Constructor(
			PoolableManager poolableManager,
			TransformActivityDataHolder transformActivityDataHolder,
			PositionDataHolder positionDataHolder,
			RotationDataHolder rotationDataHolder)
		{
			_transformActivityDataHolder = transformActivityDataHolder;
			_positionDataHolder = positionDataHolder;
			_rotationDataHolder = rotationDataHolder;
			_poolableManager = poolableManager;
			_transformActivityDataHolder.IsActive.Where(isActive => isActive == false)
				.Subscribe(_ => DespawnFromPool());
		}

		public void OnSpawned(Vector3 position, Quaternion rotation, IMemoryPool pool)
		{
			_transformActivityDataHolder.IsActive.Value = true;
			_positionDataHolder.Position.Value = position;
			_rotationDataHolder.Rotation.Value = rotation;
			_pool = pool;
			var enemyTransform = transform;
			enemyTransform.position = position;
			enemyTransform.rotation = rotation;
			_poolableManager.TriggerOnSpawned();
		}

		public void OnDespawned()
		{
			_transformActivityDataHolder.IsActive.Value = false;
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
		public class Factory : PlaceholderFactory<Vector3, Quaternion, SpellGameObjectPoolableFacade>
		{
			public SpellDefinition SpellType { get; private set; }

			[Inject]
			private void Constructor(SpellDefinition spellDefinition)
			{
				SpellType = spellDefinition;
			}
		}
	}
}