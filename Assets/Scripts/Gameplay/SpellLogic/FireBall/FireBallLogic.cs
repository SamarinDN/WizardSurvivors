using System;
using DataHolders.Transform;
using Definitions.Spells;
using JetBrains.Annotations;
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
		private TransformActivityDataHolder _transformActivityDataHolder;
		private PositionDataHolder _positionDataHolder;
		private RotationDataHolder _rotationDataHolder;

		private readonly CompositeDisposable _disposables = new();

		private readonly ReactiveProperty<Vector3> _fireBallPosition = new();
		private readonly ReactiveProperty<float> _flownDistance = new();

		public IReadOnlyReactiveProperty<Vector3> FireBallPosition => _fireBallPosition;

		[Inject]
		private void Constructor(FireBallDefinition fireBallDefinition,
			TransformActivityDataHolder transformActivityDataHolder,
			PositionDataHolder positionDataHolder,
			RotationDataHolder rotationDataHolder)
		{
			_fireBallDefinition = fireBallDefinition;
			_transformActivityDataHolder = transformActivityDataHolder;
			_positionDataHolder = positionDataHolder;
			_rotationDataHolder = rotationDataHolder;
		}

		public void OnDespawned()
		{
		}

		public void OnSpawned()
		{
			_fireBallPosition.Value = _positionDataHolder.Position.Value;
			_flownDistance.Value = 0f;
		}

		public void Initialize()
		{
			Observable.EveryUpdate()
				.Select(_ => _transformActivityDataHolder.IsActive)
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
				_positionDataHolder.Position.Value +
				_rotationDataHolder.Rotation.Value * Vector3.forward * _flownDistance.Value;
		}

		private void OnFireBallFlightFinish()
		{
			_transformActivityDataHolder.IsActive.Value = false;
		}
	}
}