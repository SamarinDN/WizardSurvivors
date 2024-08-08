using Zenject;

namespace Services.PlayerMovementService
{
	public sealed class PlayerMovementServiceInstaller : MonoInstaller<PlayerMovementServiceInstaller>
	{
		public override void InstallBindings()
		{
			Container.BindInterfacesTo<PlayerMovementService>().AsSingle().NonLazy();
		}
	}
}