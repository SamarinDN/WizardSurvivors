using Zenject;

namespace Services.InputService
{
	public sealed class PlayerInputServiceInstaller : MonoInstaller<PlayerInputServiceInstaller>
	{
		public override void InstallBindings()
		{
			Container.BindInterfacesTo<PlayerInputService>().AsSingle().NonLazy();
		}
	}
}