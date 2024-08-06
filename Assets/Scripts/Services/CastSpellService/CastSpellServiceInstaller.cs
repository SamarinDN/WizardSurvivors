using Zenject;

namespace Services.CastSpellService
{
	public sealed class CastSpellServiceInstaller : MonoInstaller<CastSpellServiceInstaller>
	{
		public override void InstallBindings()
		{
			Container.Bind<SpellCastHandler>().AsSingle();

			Container.BindInterfacesTo<CastSpellService>().AsSingle().NonLazy();
		}
	}
}