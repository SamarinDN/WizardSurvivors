using DataHolders.Transform;
using Definitions.Enemies;
using Definitions.LevelSettings;
using Gameplay.UnitBehaviourLogic.ApproachingToPlayerLogic;
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

		private void BindEnemyFactory(UnitDefinition enemy)
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

		private static void InstallEnemy(DiContainer subContainer, UnitDefinition enemy)
		{
			subContainer.Bind(enemy.GetType()).FromScriptableObject(enemy).AsSingle();
			subContainer.Bind<EnemyGameObjectPoolableFacade>().FromNewComponentOnRoot().AsSingle();
			subContainer.Bind<PoolableManager>().AsSingle();
			subContainer.Bind<TransformActivityDataHolder>().AsSingle();
			subContainer.Bind<PositionDataHolder>().AsSingle();
			subContainer.Bind<RotationDataHolder>().AsSingle();
			//TODO: В случае когда поведений будет больше одного
			//необходимо будет сделать систему биндинга UnitDefinition и логики поведения юнитов
			subContainer.BindInterfacesAndSelfTo<ApproachingToPlayerLogic>().AsSingle().NonLazy();
		}
	}
}