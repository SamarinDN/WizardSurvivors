namespace Definitions.Spells
{
	public interface IDamagingSpellDefinition : ISpellDefinition
	{
		public float SpellDamage { get; }
	}
}