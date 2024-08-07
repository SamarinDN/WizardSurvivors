using System;
using DataHolders.Transform;
using Definitions.Spells;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.SpellLogic.FireNova
{
	[UsedImplicitly]
	[SpellDefinitionInfo(typeof(FireNovaDefinition))]
	public sealed class FireNovaLogic : IFireNovaLogicDataHolder, IPoolable, IInitializable, IDisposable
	{
		private FireNovaDefinition _fireNovaDefinition;
		private TransformActivityDataHolder _transformActivityDataHolder;

		private readonly CompositeDisposable _disposables = new();

		private readonly ReactiveProperty<float> _novaRadius = new();

		public IReadOnlyReactiveProperty<float> NovaRadius => _novaRadius;

		[Inject]
		private void Constructor(FireNovaDefinition fireNovaDefinition,
			TransformActivityDataHolder transformActivityDataHolder)
		{
			_fireNovaDefinition = fireNovaDefinition;
			_transformActivityDataHolder = transformActivityDataHolder;
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
				.Select(_ => _transformActivityDataHolder.IsActive)
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
			_transformActivityDataHolder.IsActive.Value = false;
		}
	}
}