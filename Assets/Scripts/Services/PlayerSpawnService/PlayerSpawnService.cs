using DataHolders;
using Definitions.Enemies;
using JetBrains.Annotations;

namespace Services.PlayerSpawnService
{
	[UsedImplicitly]
	internal sealed class PlayerSpawnService : IPlayerSpawnService
	{
		public PlayerSpawnService(PlayerDefinition playerDefinition,
			HealthPointsDataHolder healthPointsDataHolder)
		{
			healthPointsDataHolder.MaxHealth.Value = playerDefinition.HealthPoints;
			healthPointsDataHolder.CurrentHealth.Value = playerDefinition.HealthPoints;
		}
	}
}