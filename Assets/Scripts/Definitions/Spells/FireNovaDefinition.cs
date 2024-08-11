using UnityEngine;

namespace Definitions.Spells
{
	[CreateAssetMenu(fileName = "FireNova", menuName = "Definitions/Spells/FireNova")]
	public sealed class FireNovaDefinition : DamagingSpellDefinition
	{
		[Header("FireNova stats")]
		[SerializeField, Min(0f)]
		private float novaSpreadSpeed;

		[SerializeField, Min(0f)]
		private float novaMaxRadius;

		public float NovaSpreadSpeed => novaSpreadSpeed;
		public float NovaMaxRadius => novaMaxRadius;
	}
}