using JetBrains.Annotations;
using UniRx;
using UnityEngine;

namespace Services.CastSpellService.SpellContainer
{
	[UsedImplicitly]
	public class CastPositionStateHolder
	{
		public readonly ReactiveProperty<Vector3> CastPosition = new();
	}
}