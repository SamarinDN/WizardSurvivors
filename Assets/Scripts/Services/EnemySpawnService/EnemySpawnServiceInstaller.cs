using DataHolders;
using DataHolders.Transform;
using Definitions.Units;
using Definitions.LevelSettings;
using Gameplay.UnitBehaviourLogic.ApproachingToPlayerLogic;
using Handlers.Enemy;
using Handlers.Units;
using UnityEngine;
using Zenject;

namespace Services.EnemySpawnService
{
	public sealed class EnemySpawnServiceInstaller : MonoInstaller<EnemySpawnServiceInstaller>
	{
		[SerializeField]
		private LevelSettingsDefinition levelSettings;

		public override void InstallBindings()
		{
			Container.Bind<CountOfEnemiesAtLevelDataHolder>().AsSingle();
			Container.Bind<EnemySpawnHandler>().AsSingle();
			BindEnemyFactories();
			Container.Bind<LevelSettingsDefinition>().FromScriptableObject(levelSettings).AsSingle().NonLazy();
			Container.BindInterfacesTo<EnemySpawnService>().AsSingle().NonLazy();
		}

		private void BindEnemyFactories()
		{
			foreach (var unit in levelSettings.AvailableEnemiesOnLevel)
			{
				BindEnemyFactory(unit);
			}
		}

		private void BindEnemyFactory(SimpleEnemyDefinition enemy)
		{
			Container
				.BindFactory<Vector3, Quaternion, EnemyGameObjectPoolableFacade,
					EnemyGameObjectPoolableFacade.Factory>()
				.WithFactoryArguments(enemy)
				.FromMonoPoolableMemoryPool(poolBind => poolBind
					.FromSubContainerResolve()
					.ByNewPrefabMethod(_ => enemy.UnitView, container => InstallEnemy(container, enemy))
					.UnderTransformGroup($"[EnemyPool - {enemy.name}]"));
		}

		private static void InstallEnemy(DiContainer subContainer, SimpleEnemyDefinition enemy)
		{
			// Биндинг баланса юнита
			subContainer.BindInterfacesAndSelfTo(enemy.GetType()).FromScriptableObject(enemy).AsSingle();
			// Биндинг классов пула
			subContainer.Bind<EnemyGameObjectPoolableFacade>().FromNewComponentOnRoot().AsSingle();
			subContainer.Bind<PoolableManager>().AsSingle();
			// Биндинг данных юнита
			subContainer.Bind<TransformActivityDataHolder>().AsSingle();
			subContainer.Bind<PositionDataHolder>().AsSingle();
			subContainer.Bind<RotationDataHolder>().AsSingle();
			subContainer.Bind<HealthPointsDataHolder>().AsSingle();
			subContainer.Bind<ReceivedDamageDataHolder>().AsSingle();
			subContainer.Bind<InvincibilityDataHolder>().AsSingle();
			// Биндинг обработчиков данных юнита
			subContainer.BindInterfacesAndSelfTo<EnemyHealthRestoreOnSpawnHandler>().AsSingle().NonLazy();
			subContainer.BindInstance(enemy.HealthPoints)
				.WhenInjectedInto<EnemyHealthRestoreOnSpawnHandler>();

			subContainer.BindInterfacesAndSelfTo<TakeDamageHandler>().AsSingle().NonLazy();
			subContainer.BindInstance(enemy.DamageReductionMultiplier)
				.WhenInjectedInto<TakeDamageHandler>();

			subContainer.BindInterfacesAndSelfTo<EnemyDeathHandler>().AsSingle().NonLazy();

			subContainer.Bind<InvincibilityAfterGettingHitHandler>().AsSingle().NonLazy();
			subContainer.BindInstance(enemy.SecondsInvincibilityAfterGettingHit)
				.WhenInjectedInto<InvincibilityAfterGettingHitHandler>();

			//TODO: В случае когда поведений будет больше одного
			//необходимо будет сделать систему биндинга UnitDefinition и логики поведения юнитов
			subContainer.BindInterfacesAndSelfTo<ApproachingToPlayerLogic>().AsSingle().NonLazy();
		}
	}
}