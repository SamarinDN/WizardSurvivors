using Handlers.Enemy;
using UnityEngine;
using Zenject;

namespace Gameplay.Collision
{
	public sealed class EnemyCollisionReceiver : MonoBehaviour
	{
		private IEnemyHealthHandler _enemyHealthHandler;

		[Inject]
		private void Constructor(IEnemyHealthHandler enemyHealthHandler)
		{
			_enemyHealthHandler = enemyHealthHandler;
		}

		public void OnReceiverCollision(float damage)
		{
			_enemyHealthHandler.TakeDamage(damage);
		}
	}
}