using JetBrains.Annotations;
using UniRx;
using UnityEngine;

namespace DataHolders.Transform
{
	[UsedImplicitly]
	public class PositionDataHolder
	{
		public readonly ReactiveProperty<Vector3> Position = new();
	}
}