using UniRx;
using UnityEngine;

namespace Services.PlayerMovementService
{
	public interface IPlayerMovementService
	{
		public IReadOnlyReactiveProperty<Vector3> PlayerPosition { get; }
		public IReadOnlyReactiveProperty<Quaternion> PlayerRotation { get; }
	}
}