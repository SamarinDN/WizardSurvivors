using Presets.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Services.InputService
{
	[RequireComponent(typeof(PlayerInput))]
	public class GameplayInputProvider : MonoBehaviour, GameplayInputActions.IPlayerActions
	{
		public void OnMove(InputAction.CallbackContext context)
		{
			Debug.Log($"OnMove: {context.ReadValue<Vector2>()}");
		}

		public void OnRotate(InputAction.CallbackContext context)
		{
			Debug.Log($"OnRotate: {context.ReadValue<Vector2>()}");
		}

		public void OnCastSpell(InputAction.CallbackContext context)
		{
			Debug.Log($"OnCastSpell: {context.ReadValueAsButton()}");
		}

		public void OnSelectPreviousSpell(InputAction.CallbackContext context)
		{
			Debug.Log($"OnSelectPreviousSpell: {context.ReadValueAsButton()}");
		}

		public void OnSelectNextSpell(InputAction.CallbackContext context)
		{
			Debug.Log($"OnSelectNextSpell: {context.ReadValueAsButton()}");
		}
	}
}