using Definitions.Spells;
using JetBrains.Annotations;
using UnityEngine;

namespace Services.CastSpellService
{
	[UsedImplicitly]
	internal sealed class SpellCastHandler
	{
		private readonly SpellGameObjectPoolableFacade.Factory[] _factories;

		public SpellCastHandler(SpellGameObjectPoolableFacade.Factory[] factories)
		{
			_factories = factories;
		}

		public void CastSpell(SpellDefinition spellDefinition, Vector3 castPosition)
		{
			foreach (var factory in _factories)
			{
				if (factory.SpellType != spellDefinition)
				{
					continue;
				}

				factory.Create(castPosition);
				return;
			}
		}
	}
}