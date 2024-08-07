using Gameplay.SpellLogic.FireBall;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.View.Spells
{
	public sealed class FireBallView : SpellView
	{
		private IFireBallLogicDataHolder _fireBallLogicDataHolder;

		[Inject]
		private void Constructor(IFireBallLogicDataHolder fireBallLogicDataHolder)
		{
			_fireBallLogicDataHolder = fireBallLogicDataHolder;
		}

		private void Awake()
		{
			_fireBallLogicDataHolder.FireBallPosition.Subscribe(OnFireBallPositionChanged).AddTo(this);
		}

		private void OnFireBallPositionChanged(Vector3 position)
		{
			transform.position = position;
		}
	}
}