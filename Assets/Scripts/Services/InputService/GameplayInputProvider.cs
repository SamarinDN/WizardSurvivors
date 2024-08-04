using Presets.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Services.InputService
{
	[RequireComponent(typeof(PlayerInput))]
	public sealed class GameplayInputProvider : MonoBehaviour, GameplayInputActions.IPlayerActions
	{
		private IPlayerInputPropertyHolder _playerInputService;

		[Inject]
		private void Constructor(IPlayerInputPropertyHolder playerInputService)
		{
			_playerInputService = playerInputService;
		}

		public void OnMove(InputAction.CallbackContext context)
		{
			_playerInputService.MoveDirectionInternal.Value = context.ReadValue<Vector2>().y;
		}

		public void OnRotate(InputAction.CallbackContext context)
		{
			_playerInputService.RotateDirectionInternal.Value = context.ReadValue<Vector2>().x;
		}

		public void OnCastSpell(InputAction.CallbackContext context)
		{
			_playerInputService.CastSpellInternal.Execute();
		}

		public void OnSelectPreviousSpell(InputAction.CallbackContext context)
		{
			_playerInputService.SelectPreviousSpellInternal.Execute();
		}

		public void OnSelectNextSpell(InputAction.CallbackContext context)
		{
			_playerInputService.SelectNextSpellInternal.Execute();
		}
	}
}