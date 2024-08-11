using DataHolders;
using Zenject;

namespace Services.GameRestartService
{
	public sealed class GameRestartServiceInstaller : MonoInstaller<GameRestartServiceInstaller>
	{
		public override void InstallBindings()
		{
			Container.Bind<GameStateDataHolder>().AsSingle().NonLazy();

			Container.BindInterfacesTo<GameRestartService>().AsSingle().NonLazy();
		}
	}
}