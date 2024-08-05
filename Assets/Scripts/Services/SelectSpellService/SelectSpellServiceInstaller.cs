using System.Collections.Generic;
using Definitions.Spells;
using UnityEngine;
using Zenject;

namespace Services.SelectSpellService
{
	public sealed class SelectSpellServiceInstaller : MonoInstaller<SelectSpellServiceInstaller>
	{
		[SerializeField]
		private List<SpellDefinition> spells = new();

		public override void InstallBindings()
		{
			Container.BindInterfacesTo<SelectSpellService>()
				.AsSingle()
				.WithArguments(spells)
				.NonLazy();
		}
	}
}