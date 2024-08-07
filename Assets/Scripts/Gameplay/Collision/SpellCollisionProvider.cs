using Definitions.Spells;
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

		private IDamagingSpellDefinition _damagingSpellDefinition;

		[Inject]
		private void Constructor(IDamagingSpellDefinition damagingSpellDefinition)
		{
			_damagingSpellDefinition = damagingSpellDefinition;
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

			Debug.Log($"Spell {name} provide collision to Enemy. Damage {_damagingSpellDefinition.SpellDamage}");
			receiver.OnReceiverCollision();
		}
	}
}