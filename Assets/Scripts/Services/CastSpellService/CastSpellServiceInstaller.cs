using Definitions.Spells;
using UnityEngine;
using Zenject;

namespace Services.CastSpellService
{
	public sealed class CastSpellServiceInstaller : MonoInstaller<CastSpellServiceInstaller>
	{
		[SerializeField]
		private SpellBook spellBook;

		public override void InstallBindings()
		{
			Container.Bind<SpellCastHandler>().AsSingle();
			BindSpellFactories();
			Container.BindInterfacesTo<CastSpellService>().AsSingle().NonLazy();
		}

		private void BindSpellFactories()
		{
			foreach (var spell in spellBook.Spells)
			{
				BindSpellFactory(spell);
			}
		}

		private void BindSpellFactory(SpellDefinition spell)
		{
			Container.BindFactory<Vector3, SpellGameObjectPoolableFacade, SpellGameObjectPoolableFacade.Factory>()
				.WithFactoryArguments(spell)
				.FromMonoPoolableMemoryPool(poolBind => poolBind
					.FromSubContainerResolve()
					.ByNewGameObjectMethod(InstallSpell)
					.UnderTransformGroup($"[SpellPool - {spell.name}]"));
		}

		private static void InstallSpell(DiContainer subContainer)
		{
			subContainer.Bind<SpellGameObjectPoolableFacade>().FromNewComponentOnRoot().AsSingle();
			subContainer.Bind<PoolableManager>().AsSingle();
		}
	}
}