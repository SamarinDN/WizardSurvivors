using DataHolders;
using TMPro;
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

		[SerializeField]
		private TMP_Text healthLabel;

		private HealthPointsDataHolder _healthPointsDataHolder;

		[Inject]
		private void Constructor(HealthPointsDataHolder healthPointsDataHolder)
		{
			_healthPointsDataHolder = healthPointsDataHolder;
			healthPointsDataHolder.CurrentHealth.Subscribe(OnHealthChanged);
		}

		private void OnHealthChanged(float currentHealth)
		{
			if (_healthPointsDataHolder.MaxHealth.Value == 0)
			{
				slider.normalizedValue = 0;
				healthLabel.text = "";
				return;
			}

			if (currentHealth < 0)
			{
				currentHealth = 0;
			}

			var maxHealth = _healthPointsDataHolder.MaxHealth.Value;
			slider.normalizedValue = currentHealth / maxHealth;
			healthLabel.text = $"{currentHealth:0} / {maxHealth}";
		}
	}
}