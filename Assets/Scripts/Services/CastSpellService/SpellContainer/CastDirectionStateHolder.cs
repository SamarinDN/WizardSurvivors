using JetBrains.Annotations;
using UniRx;
using UnityEngine;

namespace Services.CastSpellService.SpellContainer
{
	[UsedImplicitly]
	public class CastDirectionStateHolder
	{
		public readonly ReactiveProperty<Quaternion> CastDirection = new();
	}
}