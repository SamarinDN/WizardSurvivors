using DataHolders;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.Collision
{
	public sealed class PlayerCollisionReceiver : MonoBehaviour
	{
		[SerializeField]
		private Collider colliderComponent;

		private ReceivedDamageDataHolder _receivedDamageDataHolder;
		private InvincibilityDataHolder _invincibilityDataHolder;

		[Inject]
		private void Constructor(ReceivedDamageDataHolder receivedDamageDataHolder,
			InvincibilityDataHolder invincibilityDataHolder)
		{
			_receivedDamageDataHolder = receivedDamageDataHolder;
			_invincibilityDataHolder = invincibilityDataHolder;
		}

		private void Start()
		{
			_invincibilityDataHolder.IsInvincibility
				.Subscribe(isPlayerInvincibility => colliderComponent.enabled = !isPlayerInvincibility)
				.AddTo(this);
		}

		public void OnReceiverCollision(float damage)
		{
			_receivedDamageDataHolder.Damage.Execute(damage);
		}
	}
}