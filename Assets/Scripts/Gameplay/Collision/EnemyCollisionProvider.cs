using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Gameplay.Collision
{
	public sealed class EnemyCollisionProvider : MonoBehaviour
	{
		[SerializeField]
		private Collider colliderComponent;

		[Inject]
		private void Constructor()
		{
		}

		private void Start()
		{
			colliderComponent.OnTriggerEnterAsObservable().Subscribe(OnColliderTriggerEnter).AddTo(this);
		}

		private void OnColliderTriggerEnter(Collider other)
		{
			if (other.TryGetComponent<PlayerCollisionReceiver>(out var receiver))
			{
				Debug.Log($"Enemy {name} provide collision to Player");
				receiver.OnReceiverCollision();
			}
		}
	}
}