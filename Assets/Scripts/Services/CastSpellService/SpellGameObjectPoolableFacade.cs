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
		private PoolableManager _poolableManager;
		private IMemoryPool _pool;

		private ReactiveProperty<bool> _isSpellActive;

		[Inject]
		private void Constructor(PoolableManager poolableManager, ReactiveProperty<bool> isSpellActive)
		{
			_isSpellActive = isSpellActive;
			_poolableManager = poolableManager;
			_isSpellActive.Where(isActive => isActive == false).Subscribe(_ => DespawnFromPool());
		}

		public void OnSpawned(Vector3 casterPosition, IMemoryPool pool)
		{
			_isSpellActive.Value = true;
			_pool = pool;
			transform.position = casterPosition;
			_poolableManager.TriggerOnSpawned();
		}

		public void OnDespawned()
		{
			_isSpellActive.Value = false;
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