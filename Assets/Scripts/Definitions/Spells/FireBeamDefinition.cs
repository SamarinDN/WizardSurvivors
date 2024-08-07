using UnityEngine;

namespace Definitions.Spells
{
	[CreateAssetMenu(fileName = "FireBeam", menuName = "Definitions/Spells/FireBeam")]
	public sealed class FireBeamDefinition : DamagingSpellDefinition
	{
		[Header("FireBeam stats")]
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