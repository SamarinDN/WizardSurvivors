using System;
using Definitions.Spells;
using JetBrains.Annotations;
using Services.InputService;
using UniRx;
using Zenject;

namespace Services.SelectSpellService
{
	[UsedImplicitly]
	internal sealed class SelectSpellService : ISelectSpellService, IInitializable, IDisposable
	{
		private readonly IPlayerInputService _playerInputService;
		private readonly ReactiveCollection<SpellDefinition> _availableSpells;

		private readonly CompositeDisposable _disposables = new();

		public IReadOnlyReactiveCollection<SpellDefinition> AvailableSpells => _availableSpells;

		public SelectSpellService(IPlayerInputService playerInputService, SpellBook spellBook)
		{
			_playerInputService = playerInputService;
			_availableSpells = spellBook.Spells.ToReactiveCollection();
		}

		public void Initialize()
		{
			_playerInputService.SelectNextSpell.Subscribe(MoveNextSpell).AddTo(_disposables);
			_playerInputService.SelectPreviousSpell.Subscribe(MovePreviousSpell).AddTo(_disposables);
		}

		public void Dispose()
		{
			_disposables.Clear();
		}

		private void MoveNextSpell(Unit _)
		{
			if (_availableSpells.Count < 1)
			{
				return;
			}

			_availableSpells.Move(0, _availableSpells.Count - 1);
		}

		private void MovePreviousSpell(Unit _)
		{
			if (_availableSpells.Count < 1)
			{
				return;
			}

			_availableSpells.Move(_availableSpells.Count - 1, 0);
		}
	}
}