using System;
using Definitions.Units;
using JetBrains.Annotations;
using Services.InputService;
using Services.LevelBoundsService;
using UniRx;
using UnityEngine;
using Zenject;

namespace Services.PlayerMovementService
{
	[UsedImplicitly]
	public sealed class PlayerMovementService : IPlayerMovementService, IInitializable, IDisposable
	{
		private readonly IPlayerInputService _playerInputService;
		private readonly ILevelBoundsProvider _levelBoundsProvider;
		private readonly PlayerDefinition _playerDefinition;

		private readonly CompositeDisposable _disposables = new();

		private readonly ReactiveProperty<Vector3> _playerPosition = new();
		private readonly ReactiveProperty<Quaternion> _playerRotation = new();

		private float _eulerRotation;

		public IReadOnlyReactiveProperty<Vector3> PlayerPosition => _playerPosition;
		public IReadOnlyReactiveProperty<Quaternion> PlayerRotation => _playerRotation;

		public PlayerMovementService(IPlayerInputService playerInputService,
			[Inject(Optional = true)] ILevelBoundsProvider levelBoundsProvider,
			PlayerDefinition playerDefinition)
		{
			_playerInputService = playerInputService;
			_levelBoundsProvider = levelBoundsProvider;
			_playerDefinition = playerDefinition;
		}

		public void Initialize()
		{
			Observable.EveryUpdate()
				.Select(_ => _playerInputService.MoveDirection)
				.Where(moveDirection => moveDirection.Value != 0)
				.Subscribe(x => Move(x.Value))
				.AddTo(_disposables);

			Observable.EveryUpdate()
				.Select(_ => _playerInputService.RotateDirection)
				.Where(rotateDirection => rotateDirection.Value != 0)
				.Subscribe(x => Rotate(x.Value))
				.AddTo(_disposables);
		}

		public void Dispose()
		{
			_disposables.Clear();
		}

		private void Move(float direction)
		{
			var moveDirection = new Vector3(0, 0, direction * Time.deltaTime * _playerDefinition.MovementSpeed);
			var newPosition = _playerPosition.Value + _playerRotation.Value * moveDirection;
			newPosition = TryBoundPosition(newPosition);
			_playerPosition.Value = newPosition;
		}

		private void Rotate(float direction)
		{
			var eulerRotation = _playerRotation.Value.eulerAngles.y +
			                    direction * Time.deltaTime * _playerDefinition.RotationSpeed;
			_playerRotation.Value = Quaternion.Euler(0, eulerRotation, 0);
		}

		private Vector3 TryBoundPosition(Vector3 position)
		{
			if (_levelBoundsProvider == null)
			{
				return position;
			}

			var bounds = _levelBoundsProvider.Bounds;
			position.x = Mathf.Clamp(position.x, -bounds.x, bounds.x);
			position.z = Mathf.Clamp(position.z, -bounds.y, bounds.y);
			return position;
		}
	}
}