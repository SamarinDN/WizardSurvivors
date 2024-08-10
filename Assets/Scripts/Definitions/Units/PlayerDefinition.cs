using UnityEngine;

namespace Definitions.Units
{
	[CreateAssetMenu(fileName = "Player", menuName = "Definitions/Units/Player")]
	public sealed class PlayerDefinition : BaseGroundMovingUnitDefinition
	{
		[Header("Player stats")]
		[SerializeField, Min(0f)]
		private float secondsInvincibilityAfterGettingHit;

		public float SecondsInvincibilityAfterGettingHit => secondsInvincibilityAfterGettingHit;
	}
}