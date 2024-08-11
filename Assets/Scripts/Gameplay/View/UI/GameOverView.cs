using DataHolders;
using UniRx;
using UnityEngine;
using Zenject;


namespace Gameplay.View.UI
{
	public sealed class GameOverView : MonoBehaviour
	{
		[SerializeField]
		private Transform gameOverLabel;

		[SerializeField]
		private Transform pressRestartLabel;

		[Inject]
		private void Constructor(GameStateDataHolder gameStateDataHolder)
		{
			gameStateDataHolder.GameStart
				.Subscribe(_ => SetLabelsState(false));
			gameStateDataHolder.GameEnd
				.Subscribe(_ => SetLabelsState(true));
		}

		private void SetLabelsState(bool isActive)
		{
			gameOverLabel.gameObject.SetActive(isActive);
			pressRestartLabel.gameObject.SetActive(isActive);
		}
	}
}