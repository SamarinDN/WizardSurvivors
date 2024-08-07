using Definitions.Spells;
using UnityEngine;
using Zenject;

namespace Services.SpellBookService
{
	[CreateAssetMenu(fileName = "SpellBookServiceInstaller", menuName = "Installers/SpellBookService")]
	public sealed class SpellBookServiceInstaller : ScriptableObjectInstaller<SpellBookServiceInstaller>
	{
		[SerializeField]
		private SpellBook spellBook;

		public override void InstallBindings()
		{
			Container.Bind<SpellBook>().FromScriptableObject(spellBook).AsSingle().NonLazy();
			foreach (var spell in spellBook.Spells)
			{
				Container.Bind(spell.GetType()).FromScriptableObject(spell).AsSingle().NonLazy();
			}
		}
	}
}