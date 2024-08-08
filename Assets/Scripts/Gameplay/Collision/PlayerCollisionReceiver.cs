using DataHolders;
using UnityEngine;
using Zenject;

namespace Gameplay.Collision
{
	public sealed class PlayerCollisionReceiver : MonoBehaviour
	{
		private ReceivedDamageDataHolder _receivedDamageDataHolder;

		[Inject]
		private void Constructor(ReceivedDamageDataHolder receivedDamageDataHolder)
		{
			_receivedDamageDataHolder = receivedDamageDataHolder;
		}

		public void OnReceiverCollision(float damage)
		{
			_receivedDamageDataHolder.Damage.Execute(damage);
		}
	}
}