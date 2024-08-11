using DataHolders;
using Definitions.Units;
using JetBrains.Annotations;
using UniRx;

namespace Services.PlayerSpawnService
{
	[UsedImplicitly]
	internal sealed class PlayerSpawnService : IPlayerSpawnService
	{
		private readonly float _playerDefaultHealthPoints;
		private readonly HealthPointsDataHolder _healthPointsDataHolder;

		public PlayerSpawnService(PlayerDefinition playerDefinition,
			HealthPointsDataHolder healthPointsDataHolder,
			GameStateDataHolder gameStateDataHolder)
		{
			_playerDefaultHealthPoints = playerDefinition.HealthPoints;
			_healthPointsDataHolder = healthPointsDataHolder;
			gameStateDataHolder.GameStart.Subscribe(_ => RestorePlayerHealthPoints());
		}

		private void RestorePlayerHealthPoints()
		{
			_healthPointsDataHolder.MaxHealth.Value = _playerDefaultHealthPoints;
			_healthPointsDataHolder.CurrentHealth.Value = _playerDefaultHealthPoints;
		}
	}
}