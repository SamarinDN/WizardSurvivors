using System;

namespace Gameplay.SpellLogic
{
	[AttributeUsage(AttributeTargets.Class)]
	internal sealed class SpellDefinitionInfoAttribute : Attribute
	{
		public Type DefinitionType { get; }

		public SpellDefinitionInfoAttribute(Type definitionType)
		{
			DefinitionType = definitionType;
		}
	}
}