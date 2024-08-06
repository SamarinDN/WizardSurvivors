using Definitions.Spells;
using Services.CastSpellService.SpellContainer;
using UnityEngine;
using Zenject;

namespace Services.CastSpellService
{
	public sealed class CastSpellServiceInstaller : MonoInstaller<CastSpellServiceInstaller>
	{
		private SpellBook _spellBook;

		[Inject]
		private void Constructor(SpellBook spellBook)
		{
			_spellBook = spellBook;
		}

		public override void InstallBindings()
		{
			Container.Bind<SpellCastHandler>().AsSingle();
			BindSpellFactories();
			Container.BindInterfacesTo<CastSpellService>().AsSingle().NonLazy();
		}

		private void BindSpellFactories()
		{
			foreach (var spell in _spellBook.Spells)
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
			subContainer.Bind<CastPositionStateHolder>().AsSingle();
			subContainer.Bind<SpellActivityStateHolder>().AsSingle();
		}
	}
}