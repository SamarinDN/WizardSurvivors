using Gameplay.View.Spells;
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

		[Header("FireBall view")]
		[SerializeField]
		private FireBallView spellView;

		public float FlightSpeed => flightSpeed;
		public float FlightDistance => flightDistance;
		public FireBallView SpellView => spellView;
	}
}