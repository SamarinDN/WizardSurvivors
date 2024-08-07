using System;
using Definitions.Spells;
using JetBrains.Annotations;
using Services.CastSpellService.SpellContainer;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.SpellLogic.FireNova
{
	[UsedImplicitly]
	public sealed class FireNovaLogic : IFireNovaLogicDataHolder, IPoolable, IInitializable, IDisposable
	{
		private FireNovaDefinition _fireNovaDefinition;
		private SpellActivityStateHolder _spellActivityStateHolder;

		private readonly CompositeDisposable _disposables = new();

		private readonly ReactiveProperty<float> _novaRadius = new();

		public IReadOnlyReactiveProperty<float> NovaRadius => _novaRadius;

		[Inject]
		private void Constructor(FireNovaDefinition fireNovaDefinition,
			SpellActivityStateHolder spellActivityStateHolder)
		{
			_fireNovaDefinition = fireNovaDefinition;
			_spellActivityStateHolder = spellActivityStateHolder;
		}

		public void OnDespawned()
		{
		}

		public void OnSpawned()
		{
			_novaRadius.Value = 0f;
		}

		public void Initialize()
		{
			Observable.EveryUpdate()
				.Select(_ => _spellActivityStateHolder.IsSpellActive)
				.Where(isNovaActive => isNovaActive.Value)
				.Subscribe(_ => OnNovaSpread())
				.AddTo(_disposables);

			_novaRadius.Where(x => x > _fireNovaDefinition.NovaMaxRadius)
				.Subscribe(_ => OnNovaEnd())
				.AddTo(_disposables);
		}

		public void Dispose()
		{
			_disposables.Clear();
		}

		private void OnNovaSpread()
		{
			_novaRadius.Value += _fireNovaDefinition.NovaSpreadSpeed * Time.deltaTime;
		}

		private void OnNovaEnd()
		{
			_spellActivityStateHolder.IsSpellActive.Value = false;
		}
	}
}