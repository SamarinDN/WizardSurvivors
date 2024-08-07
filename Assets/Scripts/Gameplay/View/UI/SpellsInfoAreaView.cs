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

		private ISelectSpellService _selectSpellService;

		[Inject]
		private void Constructor(ISelectSpellService selectSpellService)
		{
			_selectSpellService = selectSpellService;
		}

		private void Awake()
		{
			RedrawSpellIcons();
			_selectSpellService.AvailableSpells.ObserveMove().Subscribe(_ => RedrawSpellIcons());
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
	}
}