using Gameplay.SpellLogic;
using Gameplay.SpellLogic.FireNova;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.View.Spells
{
	public sealed class FireNovaView : SpellView
	{
		[SerializeField]
		private Transform novaTransform;

		private IFireNovaLogicDataHolder _fireNovaLogicDataHolder;

		[Inject]
		private void Constructor(IFireNovaLogicDataHolder fireNovaLogicDataHolder)
		{
			_fireNovaLogicDataHolder = fireNovaLogicDataHolder;
		}

		private void Awake()
		{
			_fireNovaLogicDataHolder.NovaRadius.Subscribe(OnNovaRadiusChanged).AddTo(this);
		}

		private void OnNovaRadiusChanged(float radius)
		{
			novaTransform.localScale = new Vector3(radius, novaTransform.localScale.y, radius);
		}
	}
}