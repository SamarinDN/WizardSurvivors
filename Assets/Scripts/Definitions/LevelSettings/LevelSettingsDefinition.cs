using System.Collections.Generic;
using Definitions.Units;
using UnityEngine;

namespace Definitions.LevelSettings
{
	[CreateAssetMenu(fileName = "LevelSettings", menuName = "Definitions/LevelSettings")]
	public sealed class LevelSettingsDefinition : ScriptableObject
	{
		[Header("Enemy types on the level")]
		[SerializeField]
		private List<SimpleEnemyDefinition> availableEnemiesOnLevel;

		[Header("Maximum number of enemies at the same time")]
		[SerializeField, Min(0f)]
		private int maximumEnemiesCountInLevel;

		[Header("Spawn interval if enemies limit is not reached")]
		[SerializeField, Min(0f)]
		private float spawnEnemiesInterval;


		public IReadOnlyList<SimpleEnemyDefinition> AvailableEnemiesOnLevel => availableEnemiesOnLevel;
		public int MaximumEnemiesCountInLevel => maximumEnemiesCountInLevel;
		public float SpawnEnemiesInterval => spawnEnemiesInterval;
	}
}