using System;
using System.Collections.Generic;
using System.Reflection;

namespace Gameplay.SpellLogic
{
	public static class SpellLogicBinder
	{
		private static readonly Dictionary<Type, Type> Bindings = new();

		public static IReadOnlyDictionary<Type, Type> SpellBindings => Bindings;

		public static void Reinitialize()
		{
			Bindings.Clear();
			var spellLogicAssembly = Assembly.GetExecutingAssembly();
			var types = spellLogicAssembly.GetTypes();
			foreach (var type in types)
			{
				var attributes = type.GetCustomAttributes(false);
				foreach (var attribute in attributes)
				{
					if (attribute is SpellDefinitionInfoAttribute spellDefinition)
					{
						Bindings.Add(spellDefinition.DefinitionType, type);
					}
				}
			}
		}
	}
}