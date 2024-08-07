using System;
using Definitions.Spells;
using JetBrains.Annotations;
using Services.CastSpellService.SpellContainer;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.SpellLogic.FireBall
{
	[UsedImplicitly]
	[SpellDefinitionInfo(typeof(FireBallDefinition))]
	public sealed class FireBallLogic : IFireBallLogicDataHolder, IPoolable, IInitializable, IDisposable
	{
		private FireBallDefinition _fireBallDefinition;
		private SpellActivityStateHolder _spellActivityStateHolder;
		private CastPositionStateHolder _castPositionStateHolder;
		private CastDirectionStateHolder _castDirectionStateHolder;

		private readonly CompositeDisposable _disposables = new();

		private readonly ReactiveProperty<Vector3> _fireBallPosition = new();
		private readonly ReactiveProperty<float> _flownDistance = new();

		public IReadOnlyReactiveProperty<Vector3> FireBallPosition => _fireBallPosition;

		[Inject]
		private void Constructor(FireBallDefinition fireBallDefinition,
			SpellActivityStateHolder spellActivityStateHolder,
			CastPositionStateHolder castPositionStateHolder,
			CastDirectionStateHolder castDirectionStateHolder)
		{
			_fireBallDefinition = fireBallDefinition;
			_spellActivityStateHolder = spellActivityStateHolder;
			_castPositionStateHolder = castPositionStateHolder;
			_castDirectionStateHolder = castDirectionStateHolder;
		}

		public void OnDespawned()
		{
		}

		public void OnSpawned()
		{
			_fireBallPosition.Value = _castPositionStateHolder.CastPosition.Value;
			_flownDistance.Value = 0f;
		}

		public void Initialize()
		{
			Observable.EveryUpdate()
				.Select(_ => _spellActivityStateHolder.IsSpellActive)
				.Where(isFireBallActive => isFireBallActive.Value)
				.Subscribe(_ => OnFireBallInFlight())
				.AddTo(_disposables);

			_flownDistance.Where(distance => distance > _fireBallDefinition.FlightDistance)
				.Subscribe(_ => OnFireBallFlightFinish())
				.AddTo(_disposables);
		}

		public void Dispose()
		{
			_disposables.Clear();
		}

		private void OnFireBallInFlight()
		{
			_flownDistance.Value += _fireBallDefinition.FlightSpeed * Time.deltaTime;

			_fireBallPosition.Value =
				_castPositionStateHolder.CastPosition.Value +
				_castDirectionStateHolder.CastDirection.Value * Vector3.forward * _flownDistance.Value;
		}

		private void OnFireBallFlightFinish()
		{
			_spellActivityStateHolder.IsSpellActive.Value = false;
		}
	}
}