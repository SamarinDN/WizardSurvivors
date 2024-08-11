using UnityEngine;

namespace Definitions.Spells
{
	[CreateAssetMenu(fileName = "FireBall", menuName = "Definitions/Spells/FireBall")]
	public sealed class FireBallDefinition : DamagingSpellDefinition
	{
		[Header("FireBall stats")]
		[SerializeField, Min(0f)]
		private float flightSpeed;

		[SerializeField, Min(0f)]
		private float flightDistance;

		public float FlightSpeed => flightSpeed;
		public float FlightDistance => flightDistance;
	}
}