using System;
using DataHolders;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;
using Utility.UniRxExtensions;

namespace Handlers.Units
{
	[UsedImplicitly]
	public sealed class InvincibilityAfterGettingHitHandler : IDisposable
	{
		private IDisposable _everyUpdateCountdownSubscription;
		private readonly IDisposable _receivedDamageSubscription;
		private readonly InvincibilityDataHolder _invincibilityDataHolder;
		private readonly ReceivedDamageDataHolder _receivedDamageDataHolder;

		public InvincibilityAfterGettingHitHandler(
			float secondsInvincibilityAfterGettingHit,
			InvincibilityDataHolder invincibilityDataHolder,
			ReceivedDamageDataHolder receivedDamageDataHolder)
		{
			_invincibilityDataHolder = invincibilityDataHolder;
			_invincibilityDataHolder.InvincibilityDuration.Value = secondsInvincibilityAfterGettingHit;
			_receivedDamageSubscription = receivedDamageDataHolder.Damage
				.Subscribe(_ => TakeHit());
		}

		private void TakeHit()
		{
			if (_invincibilityDataHolder.IsInvincibility.Value)
			{
				Debug.LogError("The unit got hit while invincibility.");
				return;
			}

			_invincibilityDataHolder.IsInvincibility.Value = true;

			_everyUpdateCountdownSubscription = ObservableExt
				.EveryUpdateCountdown(_invincibilityDataHolder.InvincibilityDuration.Value).Subscribe(
					countdown => _invincibilityDataHolder.InvincibilityTimer.Value = countdown,
					() => _invincibilityDataHolder.IsInvincibility.Value = false);
		}

		public void Dispose()
		{
			_everyUpdateCountdownSubscription?.Dispose();
			_receivedDamageSubscription?.Dispose();
		}
	}
}