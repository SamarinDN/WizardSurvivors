using Definitions.Enemies;
using UnityEngine;
using Zenject;

namespace Services.PlayerMovementService
{
	public sealed class PlayerMovementServiceInstaller : MonoInstaller<PlayerMovementServiceInstaller>
	{
		[SerializeField]
		private PlayerDefinition playerDefinition;

		public override void InstallBindings()
		{
			Container.Bind<PlayerDefinition>().FromScriptableObject(playerDefinition).AsSingle();
			Container.BindInterfacesTo<PlayerMovementService>().AsSingle().NonLazy();
		}
	}
}