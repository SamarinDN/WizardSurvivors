using System;
using JetBrains.Annotations;
using Services.InputService;
using Services.PlayerMovementService;
using Services.SelectSpellService;
using UniRx;
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
			_playerInputService.CastSpell.Subscribe(TryCastSpell).AddTo(_disposables);
		}

		public void Dispose()
		{
			_disposables.Clear();
		}

		private void TryCastSpell(Unit _)
		{
			_spellCastHandler.CastSpell(_selectSpellService.AvailableSpells[0],
				_playerMovementService.PlayerPosition.Value, _playerMovementService.PlayerRotation.Value);
		}
	}
}