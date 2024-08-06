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
		private CastPositionStateHolder _castPositionStateHolder;

		[Inject]
		private void Constructor(PoolableManager poolableManager, SpellActivityStateHolder spellActivityStateHolder,
			CastPositionStateHolder castPositionStateHolder)
		{
			_spellActivityStateHolder = spellActivityStateHolder;
			_castPositionStateHolder = castPositionStateHolder;
			_poolableManager = poolableManager;
			_spellActivityStateHolder.IsSpellActive.Where(isActive => isActive == false)
				.Subscribe(_ => DespawnFromPool());
		}

		public void OnSpawned(Vector3 castPosition, IMemoryPool pool)
		{
			_spellActivityStateHolder.IsSpellActive.Value = true;
			_castPositionStateHolder.CastPosition.Value = castPosition;
			_pool = pool;
			transform.position = castPosition;
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