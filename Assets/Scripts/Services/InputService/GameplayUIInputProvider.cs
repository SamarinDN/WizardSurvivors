using Presets.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Services.InputService
{
	[RequireComponent(typeof(PlayerInput))]
	public sealed class GameplayUIInputProvider : MonoBehaviour, GameplayInputActions.IUIActions
	{
		private IPlayerUIInputPropertyHolder _playerUIInputService;

		[Inject]
		private void Constructor(IPlayerUIInputPropertyHolder playerUIInputService)
		{
			_playerUIInputService = playerUIInputService;
		}

		public void OnSubmit(InputAction.CallbackContext context)
		{
			_playerUIInputService.SubmitButtonPressedInternal.Execute();
		}
	}
}