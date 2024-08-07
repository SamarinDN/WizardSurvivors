using UnityEngine;

namespace Definitions.Enemies
{
	[CreateAssetMenu(fileName = "SimpleEnemy", menuName = "Definitions/Units/SimpleEnemyDefinition")]
	public sealed class SimpleEnemyDefinition : BaseGroundMovingUnitDefinition
	{
		[Header("Enemy stats")]
		[SerializeField]
		private float damage;

		public float Damage => damage;
	}
}