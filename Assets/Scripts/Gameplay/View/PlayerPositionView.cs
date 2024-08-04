using Services.PlayerMovementService;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.View
{
	public sealed class PlayerPositionView : MonoBehaviour
	{
		private IPlayerMovementService _playerMovementService;

		[Inject]
		private void Constructor(IPlayerMovementService playerMovementService)
		{
			_playerMovementService = playerMovementService;
		}

		private void Awake()
		{
			_playerMovementService.PlayerPosition.Subscribe(position => transform.position = position).AddTo(this);
			_playerMovementService.PlayerRotation.Subscribe(rotation => transform.rotation = rotation).AddTo(this);
		}
	}
}