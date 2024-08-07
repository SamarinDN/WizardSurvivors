using UniRx;
using UnityEngine;

namespace Gameplay.SpellLogic.FireBeam
{
	public interface IFireBeamLogicDataHolder
	{
		public IReadOnlyReactiveProperty<float> BeamLength { get; }
		public IReadOnlyReactiveProperty<Vector3> BeamStartPosition { get; }
		public IReadOnlyReactiveProperty<Quaternion> BeamDirection { get; }
	}
}