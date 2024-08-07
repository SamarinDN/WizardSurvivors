using JetBrains.Annotations;
using UniRx;
using UnityEngine;

namespace DataHolders.Transform
{
	[UsedImplicitly]
	public class RotationDataHolder
	{
		public readonly ReactiveProperty<Quaternion> Rotation = new();
	}
}