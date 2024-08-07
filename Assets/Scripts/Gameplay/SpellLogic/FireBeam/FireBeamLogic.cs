using System;
using DataHolders.Transform;
using Definitions.Spells;
using JetBrains.Annotations;
using Services.PlayerMovementService;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.SpellLogic.FireBeam
{
	[UsedImplicitly]
	[SpellDefinitionInfo(typeof(FireBeamDefinition))]
	public sealed class FireBeamLogic : IFireBeamLogicDataHolder, IPoolable, IInitializable, IDisposable
	{
		private FireBeamDefinition _fireBeamDefinition;
		private TransformActivityDataHolder _transformActivityDataHolder;
		private IPlayerMovementService _playerMovementService;

		private readonly CompositeDisposable _disposables = new();

		private float _beamRawLength;

		private readonly ReactiveProperty<float> _beamLifetime = new();
		private readonly ReactiveProperty<float> _beamLength = new();
		private readonly ReactiveProperty<Vector3> _beamPosition = new();
		private readonly ReactiveProperty<Quaternion> _beamDirection = new();

		public IReadOnlyReactiveProperty<float> BeamLength => _beamLength;
		public IReadOnlyReactiveProperty<Vector3> BeamStartPosition => _beamPosition;
		public IReadOnlyReactiveProperty<Quaternion> BeamDirection => _beamDirection;

		[Inject]
		private void Constructor(FireBeamDefinition fireBeamDefinition,
			TransformActivityDataHolder transformActivityDataHolder,
			IPlayerMovementService playerMovementService)
		{
			_fireBeamDefinition = fireBeamDefinition;
			_transformActivityDataHolder = transformActivityDataHolder;
			_playerMovementService = playerMovementService;
		}

		public void OnDespawned()
		{
		}

		public void OnSpawned()
		{
			_beamLifetime.Value = 0f;
			_beamRawLength = 0f;
			_beamLength.Value = 0f;
			UpdateBeamTransform();
		}

		public void Initialize()
		{
			Observable.EveryUpdate()
				.Select(_ => _transformActivityDataHolder.IsActive)
				.Where(isFireBeamActive => isFireBeamActive.Value)
				.Subscribe(_ => OnBeamGrowing())
				.AddTo(_disposables);

			_beamLifetime.Where(lifetime => lifetime > _fireBeamDefinition.BeamSecondsLifetime)
				.Subscribe(_ => OnBeamFinish())
				.AddTo(_disposables);
		}

		public void Dispose()
		{
			_disposables.Clear();
		}

		private void OnBeamGrowing()
		{
			UpdateBeamTransform();

			_beamLifetime.Value += Time.deltaTime;

			var beamLengthDelta = _fireBeamDefinition.BeamLaunchSpeed * Time.deltaTime;
			_beamRawLength = _beamLifetime.Value < (_fireBeamDefinition.BeamSecondsLifetime / 2)
				? _beamRawLength + beamLengthDelta
				: _beamRawLength - beamLengthDelta;
			var maxLength = _fireBeamDefinition.BeamMaxLength;
			_beamLength.Value = _beamRawLength < maxLength ? _beamRawLength : maxLength;
		}

		private void OnBeamFinish()
		{
			_transformActivityDataHolder.IsActive.Value = false;
		}

		private void UpdateBeamTransform()
		{
			_beamPosition.Value = _playerMovementService.PlayerPosition.Value;
			_beamDirection.Value = _playerMovementService.PlayerRotation.Value;
		}
	}
}