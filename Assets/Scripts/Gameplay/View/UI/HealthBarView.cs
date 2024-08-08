using DataHolders;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Gameplay.View.UI
{
	public sealed class HealthBarView : MonoBehaviour
	{
		[SerializeField]
		private Slider slider;

		private HealthPointsDataHolder _healthPointsDataHolder;

		[Inject]
		private void Constructor(HealthPointsDataHolder healthPointsDataHolder)
		{
			_healthPointsDataHolder = healthPointsDataHolder;
			healthPointsDataHolder.CurrentHealth.Subscribe(OnNext);
		}

		private void OnNext(float _)
		{
			slider.normalizedValue =
				_healthPointsDataHolder.CurrentHealth.Value / _healthPointsDataHolder.MaxHealth.Value;
		}
	}
}