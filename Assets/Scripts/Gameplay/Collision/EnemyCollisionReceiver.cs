using UnityEngine;
using Zenject;

namespace Gameplay.Collision
{
	public sealed class EnemyCollisionReceiver : MonoBehaviour
	{
		[Inject]
		private void Constructor()
		{
		}

		public void OnReceiverCollision()
		{
			Debug.Log($"Enemy receive collision");
		}
	}
}