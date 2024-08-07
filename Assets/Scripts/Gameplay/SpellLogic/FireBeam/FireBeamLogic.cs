using System;
using Definitions.Spells;
using JetBrains.Annotations;
using Services.CastSpellService.SpellContainer;
using Services.PlayerMovementService;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.SpellLogic.FireBeam
{
	[UsedImplicitly]
	public sealed class FireBeamLogic : IFireBeamLogicDataHolder, IPoolable, IInitializable, IDisposable
	{
		private FireBeamDefinition _fireBeamDefinition;
		private SpellActivityStateHolder _spellActivityStateHolder;
		private IPlayerMovementService _playerMovementService;

		private readonly CompositeDisposable _disposables = new();

		private readonly ReactiveProperty<float> _beamLifetime = new();
		private readonly ReactiveProperty<float> _beamLength = new();
		private readonly ReactiveProperty<Vector3> _beamPosition = new();
		private readonly ReactiveProperty<Quaternion> _beamDirection = new();

		public IReadOnlyReactiveProperty<float> BeamLength => _beamLength;
		public IReadOnlyReactiveProperty<Vector3> BeamStartPosition => _beamPosition;
		public IReadOnlyReactiveProperty<Quaternion> BeamDirection => _beamDirection;

		[Inject]
		private void Constructor(FireBeamDefinition fireBeamDefinition,
			SpellActivityStateHolder spellActivityStateHolder,
			IPlayerMovementService playerMovementService)
		{
			_fireBeamDefinition = fireBeamDefinition;
			_spellActivityStateHolder = spellActivityStateHolder;
			_playerMovementService = playerMovementService;
		}

		public void OnDespawned()
		{
		}

		public void OnSpawned()
		{
			_beamLifetime.Value = 0f;
			_beamLength.Value = 0f;
			UpdateBeamTransform();
		}

		public void Initialize()
		{
			Observable.EveryUpdate()
				.Select(_ => _spellActivityStateHolder.IsSpellActive)
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
			if (_beamLength.Value < _fireBeamDefinition.BeamMaxLength)
			{
				_beamLength.Value += _fireBeamDefinition.BeamLaunchSpeed * Time.deltaTime;
			}
		}

		private void OnBeamFinish()
		{
			_spellActivityStateHolder.IsSpellActive.Value = false;
		}

		private void UpdateBeamTransform()
		{
			_beamPosition.Value = _playerMovementService.PlayerPosition.Value;
			_beamDirection.Value = _playerMovementService.PlayerRotation.Value;
		}
	}
}