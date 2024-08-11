using JetBrains.Annotations;
using UniRx;
using UnityEngine;

namespace DataHolders.Transform
{
	[UsedImplicitly]
	public sealed class RotationDataHolder
	{
		public readonly ReactiveProperty<Quaternion> Rotation = new();
	}
}