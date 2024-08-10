using DataHolders;
using Definitions.Enemies;
using UnityEngine;
using Zenject;

namespace Services.PlayerSpawnService
{
	public sealed class PlayerSpawnServiceInstaller : MonoInstaller<PlayerSpawnServiceInstaller>
	{
		[SerializeField]
		private PlayerDefinition playerDefinition;

		public override void InstallBindings()
		{
			Container.Bind<PlayerDefinition>().FromScriptableObject(playerDefinition).AsSingle();
			Container.Bind<HealthPointsDataHolder>().AsSingle();
			Container.Bind<ReceivedDamageDataHolder>().AsSingle();

			Container.Bind<PlayerTakeDamageHandler>().AsSingle().NonLazy();

			Container.BindInterfacesTo<PlayerSpawnService>().AsSingle().NonLazy();
		}
	}
}