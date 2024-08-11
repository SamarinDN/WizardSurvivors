using System;
using JetBrains.Annotations;
using Services.InputService;
using Services.PlayerMovementService;
using Services.SelectSpellService;
using UniRx;
using UnityEngine;
using Zenject;

namespace Services.CastSpellService
{
	[UsedImplicitly]
	internal sealed class CastSpellService : ICastSpellService, IInitializable, IDisposable
	{
		private readonly CompositeDisposable _disposables = new();

		private readonly IPlayerInputService _playerInputService;
		private readonly IPlayerMovementService _playerMovementService;
		private readonly ISelectSpellService _selectSpellService;
		private readonly SpellCastHandler _spellCastHandler;

		private readonly ReactiveProperty<bool> _isSpellCanBeCast = new();
		private readonly ReactiveProperty<float> _spellCooldownPercentage = new();

		private float _spellSecondsCooldown;
		private float _spellCooldownTimer;

		public IReadOnlyReactiveProperty<bool> IsSpellCanBeCast => _isSpellCanBeCast;
		public IReadOnlyReactiveProperty<float> SpellCooldownPercentage => _spellCooldownPercentage;

		public CastSpellService(
			IPlayerInputService playerInputService,
			IPlayerMovementService playerMovementService,
			ISelectSpellService selectSpellService,
			SpellCastHandler spellCastHandler)
		{
			_playerInputService = playerInputService;
			_playerMovementService = playerMovementService;
			_selectSpellService = selectSpellService;
			_spellCastHandler = spellCastHandler;
		}

		public void Initialize()
		{
			ResetCooldown();
			_playerInputService.CastSpell.Subscribe(TryCastSpell).AddTo(_disposables);

			Observable.EveryUpdate()
				.Select(_ => _isSpellCanBeCast)
				.Where(isSpellCanBeCast => !isSpellCanBeCast.Value)
				.Subscribe(_ => OnCooldownProgress())
				.AddTo(_disposables);
		}

		public void Dispose()
		{
			_disposables.Clear();
		}

		private void TryCastSpell(Unit _)
		{
			if (!_isSpellCanBeCast.Value)
			{
				return;
			}

			_isSpellCanBeCast.Value = false;
			var spell = _selectSpellService.AvailableSpells[0];
			_spellSecondsCooldown = spell.SpellSecondsCooldown;
			_spellCooldownTimer = spell.SpellSecondsCooldown;
			UpdateSpellCooldownPercentage();
			_spellCastHandler.CastSpell(spell,
				_playerMovementService.PlayerPosition.Value, _playerMovementService.PlayerRotation.Value);
		}

		private void OnCooldownProgress()
		{
			_spellCooldownTimer -= Time.deltaTime;
			UpdateSpellCooldownPercentage();
			if (_spellCooldownTimer < 0)
			{
				ResetCooldown();
			}
		}

		private void ResetCooldown()
		{
			_isSpellCanBeCast.Value = true;
			_spellCooldownTimer = 0;
			_spellCooldownPercentage.Value = 0f;
		}

		private void UpdateSpellCooldownPercentage()
		{
			var percentage = _spellCooldownTimer / _spellSecondsCooldown;
			_spellCooldownPercentage.Value = float.IsNaN(percentage) ? 0 : _spellCooldownTimer / _spellSecondsCooldown;
		}
	}
}