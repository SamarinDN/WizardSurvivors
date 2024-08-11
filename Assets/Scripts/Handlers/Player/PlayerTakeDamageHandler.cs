using System;
using DataHolders;
using JetBrains.Annotations;
using UniRx;

namespace Handlers.Player
{
	[UsedImplicitly]
	public sealed class PlayerTakeDamageHandler : IDisposable
	{
		private readonly IDisposable _receivedDamageSubscription;
		private readonly ReceivedDamageDataHolder _receivedDamageDataHolder;
		private readonly HealthPointsDataHolder _healthPointsDataHolder;
		private readonly float _damageMitigationMultiplier;

		public PlayerTakeDamageHandler(
			float damageMitigationMultiplier,
			ReceivedDamageDataHolder receivedDamageDataHolder,
			HealthPointsDataHolder healthPointsDataHolder)
		{
			_damageMitigationMultiplier = damageMitigationMultiplier;
			_healthPointsDataHolder = healthPointsDataHolder;
			_receivedDamageSubscription = receivedDamageDataHolder.Damage
				.Subscribe(TakeDamage);
		}

		private void TakeDamage(float damage)
		{
			_healthPointsDataHolder.CurrentHealth.Value -= damage * _damageMitigationMultiplier;
		}

		public void Dispose()
		{
			_receivedDamageSubscription?.Dispose();
		}
	}
}