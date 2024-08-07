using UnityEngine;

namespace Services.LevelBoundsService
{
	public interface ILevelBoundsProvider
	{
		public Vector2 Bounds { get; }
	}
}