using DataHolders.Transform;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.View
{
	public sealed class PositionView : MonoBehaviour
	{
		[Inject]
		private void Constructor(PositionDataHolder positionDataHolder)
		{
			positionDataHolder.Position.Subscribe(position => transform.position = position).AddTo(this);
		}
	}
}