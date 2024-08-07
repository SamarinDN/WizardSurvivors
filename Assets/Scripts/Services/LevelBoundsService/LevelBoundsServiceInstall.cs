using Zenject;

namespace Services.LevelBoundsService
{
	public sealed class LevelBoundsServiceInstall : MonoInstaller<LevelBoundsServiceInstall>
	{
		public override void InstallBindings()
		{
			Container.Bind<ILevelBoundsProvider>().To<LevelBoundsProvider>().FromComponentInHierarchy().AsSingle();
		}
	}
}