using System;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;
using Utility.UniRxExtensions;
using Zenject;

namespace Services.InputService
{
	[UsedImplicitly]
	internal sealed class PlayerInputService
		: IPlayerInputPropertyHolder, IPlayerInputService, IInitializable, IDisposable
	{
		public ReactiveProperty<float> MoveDirectionInternal { get; } = new();
		public ReactiveProperty<float> RotateDirectionInternal { get; } = new();
		public ReactiveCommand CastSpellInternal { get; } = new();
		public ReactiveCommand SelectPreviousSpellInternal { get; } = new();
		public ReactiveCommand SelectNextSpellInternal { get; } = new();

		public IReadOnlyReactiveProperty<float> MoveDirection => MoveDirectionInternal;
		public IReadOnlyReactiveProperty<float> RotateDirection => RotateDirectionInternal;
		public ReadOnlyReactiveCommand CastSpell { get; }
		public ReadOnlyReactiveCommand SelectPreviousSpell { get; }
		public ReadOnlyReactiveCommand SelectNextSpell { get; }

		private readonly CompositeDisposable _disposables = new();

		public PlayerInputService()
		{
			CastSpell = new ReadOnlyReactiveCommand(CastSpellInternal);
			SelectPreviousSpell = new ReadOnlyReactiveCommand(SelectPreviousSpellInternal);
			SelectNextSpell = new ReadOnlyReactiveCommand(SelectNextSpellInternal);
		}

		public void Initialize()
		{
			//TODO: Удалить. Код заглушка для проверки что инпут работает
			CastSpell
				.Subscribe(_ => Debug.Log($"CastSpell"))
				.AddTo(_disposables);

			SelectPreviousSpell
				.Subscribe(_ => Debug.Log($"SelectPreviousSpell"))
				.AddTo(_disposables);

			SelectNextSpell
				.Subscribe(_ => Debug.Log($"SelectNextSpell"))
				.AddTo(_disposables);
		}

		public void Dispose()
		{
			_disposables.Clear();
		}
	}
}