using UnityEngine;
using Zenject;

namespace Gameplay.Collision
{
	public sealed class PlayerCollisionReceiver : MonoBehaviour
	{
		[Inject]
		private void Constructor()
		{
		}

		public void OnReceiverCollision()
		{
			Debug.Log($"Player receive collision");
		}
	}
}