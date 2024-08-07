using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Gameplay.Collision
{
	public sealed class SpellCollisionProvider : MonoBehaviour
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
			if (!other.TryGetComponent<EnemyCollisionReceiver>(out var receiver))
			{
				return;
			}

			Debug.Log($"Spell {name} provide collision to Enemy");
			receiver.OnReceiverCollision();
		}
	}
}