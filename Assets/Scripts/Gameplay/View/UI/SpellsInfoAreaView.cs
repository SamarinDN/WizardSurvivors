using Services.CastSpellService;
using Services.SelectSpellService;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Gameplay.View.UI
{
	public sealed class SpellsInfoAreaView : MonoBehaviour
	{
		[SerializeField]
		private Image previousSpell;

		[SerializeField]
		private Image activeSpell;

		[SerializeField]
		private Image nextSpell;

		[SerializeField]
		private Image cooldown;

		private ISelectSpellService _selectSpellService;
		private ICastSpellService _castSpellService;

		[Inject]
		private void Constructor(ISelectSpellService selectSpellService, ICastSpellService castSpellService)
		{
			_selectSpellService = selectSpellService;
			_castSpellService = castSpellService;
		}

		private void Awake()
		{
			RedrawSpellIcons();
			_selectSpellService.AvailableSpells.ObserveMove().Subscribe(_ => RedrawSpellIcons()).AddTo(this);
			_castSpellService.SpellCooldownPercentage.Subscribe(_ => OnCooldownChanged()).AddTo(this);
		}

		private void RedrawSpellIcons()
		{
			var spells = _selectSpellService.AvailableSpells;
			if (spells.Count < 1)
			{
				Debug.LogError("SpellsInfoAreaView. The number of spells is less than 1. Skip redrawing..");
				return;
			}

			if (spells.Count == 1)
			{
				var sprite = spells[0].SpellIcon;
				previousSpell.sprite = sprite;
				activeSpell.sprite = sprite;
				nextSpell.sprite = sprite;
				return;
			}

			previousSpell.sprite = spells[^1].SpellIcon;
			activeSpell.sprite = spells[0].SpellIcon;
			nextSpell.sprite = spells[1].SpellIcon;
		}

		private void OnCooldownChanged()
		{
			cooldown.fillAmount = _castSpellService.IsSpellCanBeCast.Value
				? 0f
				: _castSpellService.SpellCooldownPercentage.Value;
		}
	}
}