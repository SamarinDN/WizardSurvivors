using UnityEngine;

namespace Definitions.Spells
{
	[CreateAssetMenu(fileName = "FireRay", menuName = "Definitions/Spells/FireRay")]
	public sealed class FireRayDefinition : DamagingSpellDefinition
	{
		[Header("FireRay stats")]
		[SerializeField, Min(0f)]
		private float beamLaunchSpeed;

		[SerializeField, Min(0f)]
		private float beamMaxLength;

		[SerializeField, Min(0f)]
		private float beamSecondsLifetime;

		public float BeamLaunchSpeed => beamLaunchSpeed;
		public float BeamMaxLength => beamMaxLength;
		public float BeamSecondsLifetime => beamSecondsLifetime;
	}
}