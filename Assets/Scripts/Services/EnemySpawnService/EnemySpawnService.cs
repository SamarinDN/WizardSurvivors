using System;
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
		private readonly EnemySpawnHandler _enemySpawnHandler;
		private readonly ILevelBoundsProvider _levelBound;

		private int _enemySpawnCount;

		public EnemySpawnService(LevelSettingsDefinition levelSettings,
			EnemySpawnHandler enemySpawnHandler,
			ILevelBoundsProvider levelBound)
		{
			_levelSettings = levelSettings;
			_enemySpawnHandler = enemySpawnHandler;
			_levelBound = levelBound;
		}

		public void Initialize()
		{
			_enemySpawnCount = 0;
			Observable.Interval(TimeSpan.FromSeconds(_levelSettings.SpawnEnemiesInterval))
				.TakeWhile(_ => _enemySpawnCount < _levelSettings.MaximumEnemiesCountInLevel)
				.Subscribe(_ => TrySpawnEnemy())
				.AddTo(_disposables);
		}

		public void Dispose()
		{
			_disposables.Clear();
		}

		private void TrySpawnEnemy()
		{
			_enemySpawnCount++;
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