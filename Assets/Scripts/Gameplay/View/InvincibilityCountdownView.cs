using DataHolders;
using DG.Tweening;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.View
{
	public sealed class InvincibilityCountdownView : MonoBehaviour
	{
		[SerializeField]
		private MeshRenderer meshRenderer;

		private InvincibilityDataHolder _invincibilityDataHolder;
		private Color _baseColor;

		[Inject]
		private void Constructor(InvincibilityDataHolder invincibilityDataHolder)
		{
			_invincibilityDataHolder = invincibilityDataHolder;
			invincibilityDataHolder.IsInvincibility
				.Where(isInvincibility => isInvincibility)
				.Subscribe(_ => OnHitReceived()).AddTo(this);
		}

		private void Awake()
		{
			var material = meshRenderer.material;
			_baseColor = material.color;
		}

		private void OnHitReceived()
		{
			meshRenderer.material.color = Color.red;
			DOTween.To(() => meshRenderer.material.color,
				color => meshRenderer.material.color = color,
				_baseColor, _invincibilityDataHolder.InvincibilityDuration.Value)
				.SetEase(Ease.InQuint)
				.SetLink(gameObject);
		}
	}
}