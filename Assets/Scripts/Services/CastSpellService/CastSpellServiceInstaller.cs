using System;
using System.Collections.Generic;
using Definitions.Spells;
using Gameplay.SpellLogic;
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
			SpellLogicBinder.Reinitialize();
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
			var spellLogicType = SpellLogicBinder.SpellBindings.GetValueOrDefault(spell.GetType());
			if (spellLogicType == null)
			{
				Debug.LogError($"Error binding spell. Logic for spell '{spell}' not found.");
				return;
			}

			Container
				.BindFactory<Vector3, Quaternion, SpellGameObjectPoolableFacade,
					SpellGameObjectPoolableFacade.Factory>()
				.WithFactoryArguments(spell)
				.FromMonoPoolableMemoryPool(poolBind => poolBind
					.FromSubContainerResolve()
					.ByNewPrefabMethod(_ => spell.SpellView, container => InstallSpell(container, spellLogicType))
					.UnderTransformGroup($"[SpellPool - {spell.name}]"));
		}

		private static void InstallSpell(DiContainer subContainer, Type spell)
		{
			subContainer.BindInterfacesTo(spell).AsSingle().NonLazy();
			subContainer.Bind<SpellGameObjectPoolableFacade>().FromNewComponentOnRoot().AsSingle();
			subContainer.Bind<PoolableManager>().AsSingle();
			subContainer.Bind<CastPositionStateHolder>().AsSingle();
			subContainer.Bind<CastDirectionStateHolder>().AsSingle();
			subContainer.Bind<SpellActivityStateHolder>().AsSingle();
		}
	}
}