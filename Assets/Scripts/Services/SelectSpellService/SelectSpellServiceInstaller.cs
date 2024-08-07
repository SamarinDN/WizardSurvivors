using Zenject;

namespace Services.SelectSpellService
{
	public sealed class SelectSpellServiceInstaller : MonoInstaller<SelectSpellServiceInstaller>
	{
		public override void InstallBindings()
		{
			Container.BindInterfacesTo<SelectSpellService>()
				.AsSingle()
				.NonLazy();
		}
	}
}