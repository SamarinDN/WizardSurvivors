using System;
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
		//TODO: Параметры игрока необходимо вынести из класса
		private const float PlayerMovementSpeed = 10;
		private const float PlayerRotationSpeed = 360;

		private readonly IPlayerInputService _playerInputService;
		private readonly ILevelBoundsProvider _levelBoundsProvider;

		private readonly CompositeDisposable _disposables = new();

		private readonly ReactiveProperty<Vector3> _playerPosition = new();
		private readonly ReactiveProperty<Quaternion> _playerRotation = new();

		private float _eulerRotation;

		public IReadOnlyReactiveProperty<Vector3> PlayerPosition => _playerPosition;
		public IReadOnlyReactiveProperty<Quaternion> PlayerRotation => _playerRotation;

		public PlayerMovementService(IPlayerInputService playerInputService,
			[Inject(Optional = true)]
			ILevelBoundsProvider levelBoundsProvider)
		{
			_playerInputService = playerInputService;
			_levelBoundsProvider = levelBoundsProvider;
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
			var moveDirection = new Vector3(0, 0, direction * Time.deltaTime * PlayerMovementSpeed);
			var newPosition = _playerPosition.Value + _playerRotation.Value * moveDirection;
			newPosition = TryBoundPosition(newPosition);
			_playerPosition.Value = newPosition;
		}

		private void Rotate(float direction)
		{
			var eulerRotation = _playerRotation.Value.eulerAngles.y + direction * Time.deltaTime * PlayerRotationSpeed;
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