using UnityEngine;

namespace Definitions.Units
{
	public abstract class BaseGroundMovingUnitDefinition : UnitDefinition, IBaseGroundMovingUnitDefinition
	{
		[Header("Base unit stats")]
		[SerializeField, Min(0f)]
		private float healthPoints;

		[SerializeField, Min(0f)]
		private float movementSpeed;

		[SerializeField, Min(0f)]
		private float rotationSpeed;

		[SerializeField, Range(0f, 1f)]
		private float damageReductionMultiplier;

		[SerializeField, Min(0f)]
		private float secondsInvincibilityAfterGettingHit;

		public float HealthPoints => healthPoints;
		public float MovementSpeed => movementSpeed;
		public float RotationSpeed => rotationSpeed;
		public float DamageReductionMultiplier => damageReductionMultiplier;
		public float SecondsInvincibilityAfterGettingHit => secondsInvincibilityAfterGettingHit;
	}
}