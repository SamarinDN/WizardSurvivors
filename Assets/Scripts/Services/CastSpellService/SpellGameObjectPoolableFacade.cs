using System;
using Definitions.Spells;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;
using Zenject;

namespace Services.CastSpellService
{
	internal sealed class SpellGameObjectPoolableFacade : MonoBehaviour, IPoolable<Vector3, IMemoryPool>, IDisposable
	{
		private PoolableManager<ReactiveProperty<bool>> _poolableManager;
		private IMemoryPool _pool;

		private readonly ReactiveProperty<bool> _isActive = new();

		[Inject]
		private void Constructor(PoolableManager<ReactiveProperty<bool>> poolableManager)
		{
			_isActive.Value = true;
			_poolableManager = poolableManager;
			_isActive.Where(isActive => isActive == false).Subscribe(_ => DespawnFromPool());
		}

		public void OnSpawned(Vector3 casterPosition, IMemoryPool pool)
		{
			_isActive.Value = true;
			_pool = pool;
			transform.position = casterPosition;
			_poolableManager.TriggerOnSpawned(_isActive);
		}

		public void OnDespawned()
		{
			_isActive.Value = false;
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
		public class Factory : PlaceholderFactory<Vector3, SpellGameObjectPoolableFacade>
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