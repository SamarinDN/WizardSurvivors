using System;
using Definitions.Spells;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Services.CastSpellService
{
	internal sealed class SpellGameObjectPoolableFacade : MonoBehaviour, IPoolable<Vector3, IMemoryPool>, IDisposable
	{
		private PoolableManager _poolableManager;
		private IMemoryPool _pool;

		[Inject]
		private void Constructor(PoolableManager poolableManager)
		{
			_poolableManager = poolableManager;
		}

		public void OnSpawned(Vector3 casterPosition, IMemoryPool pool)
		{
			_pool = pool;
			transform.position = casterPosition;
			_poolableManager.TriggerOnSpawned();
		}

		public void OnDespawned()
		{
			_pool = null;
			_poolableManager.TriggerOnDespawned();
		}

		public void Dispose()
		{
			_pool.Despawn(this);
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