using System;
using DataHolders;
using JetBrains.Annotations;
using Services.InputService;
using UniRx;
using UnityEngine;
using Zenject;

namespace Services.GameRestartService
{
	[UsedImplicitly]
	internal sealed class GameRestartService : IGameRestartService, IInitializable, IDisposable
	{
		private readonly CompositeDisposable _disposables = new();

		private readonly IPlayerUIInputService _playerUIInputService;
		private readonly GameStateDataHolder _gameStateDataHolder;
		private readonly HealthPointsDataHolder _healthPointsDataHolder;

		public GameRestartService(
			IPlayerUIInputService playerUIInputService,
			GameStateDataHolder gameStateDataHolder,
			HealthPointsDataHolder healthPointsDataHolder)
		{
			_playerUIInputService = playerUIInputService;
			_gameStateDataHolder = gameStateDataHolder;
			_healthPointsDataHolder = healthPointsDataHolder;
		}

		public void Initialize()
		{
			RestartGame();

			_playerUIInputService.SubmitButtonPressed
				.Where(_ => IsGameOver())
				.Subscribe(_ => RestartGame())
				.AddTo(_disposables);
			_healthPointsDataHolder.CurrentHealth
				.Where(_ => IsGameOver())
				.Subscribe(_ => EndGame())
				.AddTo(_disposables);
		}

		public void Dispose()
		{
			_disposables.Clear();
		}

		private bool IsGameOver()
		{
			return _healthPointsDataHolder.CurrentHealth.Value <= 0;
		}

		private void RestartGame()
		{
			_gameStateDataHolder.GameStart.Execute();
			Time.timeScale = 1f;
		}

		private void EndGame()
		{
			_gameStateDataHolder.GameEnd.Execute();
			Time.timeScale = 0f;
		}
	}
}