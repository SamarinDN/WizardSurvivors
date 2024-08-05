using System;
using JetBrains.Annotations;
using Services.InputService;
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
		private readonly ISelectSpellService _selectSpellService;

		public CastSpellService(IPlayerInputService playerInputService, ISelectSpellService selectSpellService)
		{
			_playerInputService = playerInputService;
			_selectSpellService = selectSpellService;
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
			//TODO: Необходимо реализовать логику каста заклинаний
		}
	}
}