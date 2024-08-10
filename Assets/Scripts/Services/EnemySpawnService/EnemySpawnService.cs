using System;
using DataHolders;
using Definitions.LevelSettings;
using JetBrains.Annotations;
using Services.LevelBoundsService;
using UniRx;
using UnityEngine;
using Zenject;

namespace Services.EnemySpawnService
{
	[UsedImplicitly]
	internal sealed class EnemySpawnService : IEnemySpawnService, IInitializable, IDisposable
	{
		private readonly CompositeDisposable _disposables = new();

		private readonly LevelSettingsDefinition _levelSettings;
		private readonly CountOfEnemiesAtLevelDataHolder _countOfEnemiesAtLevelDataHolder;
		private readonly EnemySpawnHandler _enemySpawnHandler;
		private readonly ILevelBoundsProvider _levelBound;

		public EnemySpawnService(LevelSettingsDefinition levelSettings,
			CountOfEnemiesAtLevelDataHolder countOfEnemiesAtLevelDataHolder,
			EnemySpawnHandler enemySpawnHandler,
			ILevelBoundsProvider levelBound)
		{
			_levelSettings = levelSettings;
			_countOfEnemiesAtLevelDataHolder = countOfEnemiesAtLevelDataHolder;
			_enemySpawnHandler = enemySpawnHandler;
			_levelBound = levelBound;
		}

		public void Initialize()
		{
			Observable.Interval(TimeSpan.FromSeconds(_levelSettings.SpawnEnemiesInterval))
				.Subscribe(_ => TrySpawnEnemy())
				.AddTo(_disposables);
		}

		public void Dispose()
		{
			_disposables.Clear();
		}

		private void TrySpawnEnemy()
		{
			if (_countOfEnemiesAtLevelDataHolder.CountOfEnemies.Value < _levelSettings.MaximumEnemiesCountInLevel)
			{
				SpawnEnemy();
			}
		}

		private void SpawnEnemy()
		{
			var enemyRandomIndex = UnityEngine.Random.Range(0, _levelSettings.AvailableEnemiesOnLevel.Count);
			var enemy = _levelSettings.AvailableEnemiesOnLevel[enemyRandomIndex];

			var position = GetRandomPosition(Vector3.zero);
			var rotation = Quaternion.LookRotation(-position);
			_enemySpawnHandler.SpawnEnemy(enemy, position, rotation);
		}

		private Vector3 GetRandomPosition(Vector3 center)
		{
			var maximumLevelDistance = Mathf.Max(_levelBound.Bounds.x, _levelBound.Bounds.y);
			// Удваиваем растояние чтобы позиция была за границами уровня
			var radiusOutsideLevel = maximumLevelDistance * 2;
			var ang = UnityEngine.Random.value * 360;
			return new Vector3(
				center.x + radiusOutsideLevel * Mathf.Sin(ang * Mathf.Deg2Rad),
				center.y,
				center.z + radiusOutsideLevel * Mathf.Cos(ang * Mathf.Deg2Rad));
		}
	}
}