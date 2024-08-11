using DataHolders.Transform;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.View
{
	public sealed class RotationView : MonoBehaviour
	{
		[Inject]
		private void Constructor(RotationDataHolder positionDataHolder)
		{
			positionDataHolder.Rotation.Subscribe(rotation => transform.rotation = rotation).AddTo(this);
		}
	}
}