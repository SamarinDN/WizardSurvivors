using DataHolders;
using Definitions.Units;
using Handlers.Units;
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
			Container.Bind<InvincibilityDataHolder>().AsSingle();

			Container.Bind<TakeDamageHandler>().AsSingle().NonLazy();
			Container.BindInstance(playerDefinition.DamageReductionMultiplier)
				.WhenInjectedInto<TakeDamageHandler>();

			Container.Bind<InvincibilityAfterGettingHitHandler>().AsSingle().NonLazy();
			Container.BindInstance(playerDefinition.SecondsInvincibilityAfterGettingHit)
				.WhenInjectedInto<InvincibilityAfterGettingHitHandler>();

			Container.BindInterfacesTo<PlayerSpawnService>().AsSingle().NonLazy();
		}
	}
}