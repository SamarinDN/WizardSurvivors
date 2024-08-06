using System;
using Definitions.Spells;
using JetBrains.Annotations;
using Services.CastSpellService.SpellContainer;
using UniRx;
using UnityEngine;
using Zenject;

namespace Services.CastSpellService
{
	internal sealed class SpellGameObjectPoolableFacade : MonoBehaviour, IPoolable<Vector3, IMemoryPool>, IDisposable
	{
		private PoolableManager _poolableManager;
		private IMemoryPool _pool;

		private SpellActivityStateHolder _spellActivityStateHolder;

		[Inject]
		private void Constructor(PoolableManager poolableManager, SpellActivityStateHolder spellActivityStateHolder)
		{
			_spellActivityStateHolder = spellActivityStateHolder;
			_poolableManager = poolableManager;
			_spellActivityStateHolder.IsSpellActive.Where(isActive => isActive == false)
				.Subscribe(_ => DespawnFromPool());
		}

		public void OnSpawned(Vector3 casterPosition, IMemoryPool pool)
		{
			_spellActivityStateHolder.IsSpellActive.Value = true;
			_pool = pool;
			transform.position = casterPosition;
			_poolableManager.TriggerOnSpawned();
		}

		public void OnDespawned()
		{
			_spellActivityStateHolder.IsSpellActive.Value = false;
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