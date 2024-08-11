using Definitions.Units;
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

		private SimpleEnemyDefinition _simpleEnemyDefinition;

		[Inject]
		private void Constructor(SimpleEnemyDefinition simpleEnemyDefinition)
		{
			_simpleEnemyDefinition = simpleEnemyDefinition;
		}

		private void Start()
		{
			colliderComponent.OnTriggerEnterAsObservable().Subscribe(OnColliderTriggerEnter).AddTo(this);
		}

		private void OnColliderTriggerEnter(Collider other)
		{
			if (other.TryGetComponent<PlayerCollisionReceiver>(out var receiver))
			{
				receiver.OnReceiverCollision(_simpleEnemyDefinition.Damage);
			}
		}
	}
}