using Gameplay.SpellLogic.FireBeam;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.View.Spells
{
	public sealed class FireBeamView : SpellView
	{
		[SerializeField]
		private Transform beamTransform;

		private IFireBeamLogicDataHolder _fireBeamLogicDataHolder;

		[Inject]
		private void Constructor(IFireBeamLogicDataHolder fireBeamLogicDataHolder)
		{
			_fireBeamLogicDataHolder = fireBeamLogicDataHolder;
		}

		private void Awake()
		{
			_fireBeamLogicDataHolder.BeamStartPosition.Subscribe(OnBeamPositionChanged).AddTo(this);
			_fireBeamLogicDataHolder.BeamDirection.Subscribe(OnBeamDirectionChanged).AddTo(this);
			_fireBeamLogicDataHolder.BeamLength.Subscribe(OnBeamLengthChanged).AddTo(this);
		}

		private void OnBeamPositionChanged(Vector3 position)
		{
			transform.position = position;
		}

		private void OnBeamDirectionChanged(Quaternion direction)
		{
			transform.rotation = direction;
		}

		private void OnBeamLengthChanged(float length)
		{
			var localPosition = beamTransform.localPosition;
			beamTransform.localPosition = new Vector3(localPosition.x, localPosition.y, length);

			var localScale = beamTransform.localScale;
			beamTransform.localScale = new Vector3(localScale.x, length, localScale.z);
		}
	}
}