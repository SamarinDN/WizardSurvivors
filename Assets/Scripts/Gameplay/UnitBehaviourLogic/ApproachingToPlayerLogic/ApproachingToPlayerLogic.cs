using System;
using DataHolders.Transform;
using Definitions.Enemies;
using JetBrains.Annotations;
using Services.PlayerMovementService;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.UnitBehaviourLogic.ApproachingToPlayerLogic
{
	[UsedImplicitly]
	public sealed class ApproachingToPlayerLogic : IInitializable, IDisposable
	{
		private SimpleEnemyDefinition _simpleEnemyDefinition;
		private IPlayerMovementService _playerMovementService;
		private PositionDataHolder _positionDataHolder;
		private RotationDataHolder _rotationDataHolder;

		private readonly CompositeDisposable _disposables = new();

		[Inject]
		private void Constructor(
			SimpleEnemyDefinition simpleEnemyDefinition,
			IPlayerMovementService playerMovementService,
			PositionDataHolder positionDataHolder,
			RotationDataHolder rotationDataHolder)
		{
			_simpleEnemyDefinition = simpleEnemyDefinition;
			_playerMovementService = playerMovementService;
			_positionDataHolder = positionDataHolder;
			_rotationDataHolder = rotationDataHolder;
		}

		public void Initialize()
		{
			Observable.EveryUpdate()
				.Subscribe(_ => MoveToPlayer())
				.AddTo(_disposables);
		}

		public void Dispose()
		{
			_disposables.Clear();
		}

		private void MoveToPlayer()
		{
			var oldRotation = _rotationDataHolder.Rotation.Value;
			// Считаем угол между направлением взгляда юнита и направлением где находится игрок
			// Если юнит смори на игрока значение будет 0,
			// если взгляд отклоняется влево или вправо значение угла будет колебаться от -180 до 180
			// где -180 / 180 - будет когда взгляд направлен в противоположенную сторону
			var angle = Vector3.SignedAngle(
				_playerMovementService.PlayerPosition.Value - _positionDataHolder.Position.Value,
				oldRotation * Vector3.forward, Vector3.up);

			var rotationDirection = Mathf.Clamp(angle, -1, 1);

			// Считаем новый угол поворота в градусах
			var eulerRotation = oldRotation.eulerAngles.y -
			                    rotationDirection * Time.deltaTime * _simpleEnemyDefinition.RotationSpeed;

			var newRotation = Quaternion.Euler(0, eulerRotation, 0);
			_rotationDataHolder.Rotation.Value = newRotation;

			// Двигаемся в новом направлении
			var forwardMoveDelta = new Vector3(0, 0, Time.deltaTime * _simpleEnemyDefinition.MovementSpeed);
			_positionDataHolder.Position.Value += newRotation * forwardMoveDelta;
		}
	}
}