using Definitions.Spells;
using UnityEngine;
using Zenject;

namespace Services.SelectSpellService
{
	public sealed class SelectSpellServiceInstaller : MonoInstaller<SelectSpellServiceInstaller>
	{
		[SerializeField]
		private SpellBook spellBook;

		public override void InstallBindings()
		{
			Container.BindInterfacesTo<SelectSpellService>()
				.AsSingle()
				.WithArguments(spellBook)
				.NonLazy();
		}
	}
}