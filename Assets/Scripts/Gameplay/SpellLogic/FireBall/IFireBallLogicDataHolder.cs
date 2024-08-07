using UniRx;
using UnityEngine;

namespace Gameplay.SpellLogic.FireBall
{
	public interface IFireBallLogicDataHolder
	{
		public IReadOnlyReactiveProperty<Vector3> FireBallPosition { get; }
	}
}