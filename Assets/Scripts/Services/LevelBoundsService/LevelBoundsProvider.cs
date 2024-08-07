using UnityEngine;

namespace Services.LevelBoundsService
{
	public sealed class LevelBoundsProvider : MonoBehaviour, ILevelBoundsProvider
	{
		[SerializeField]
		private Vector2 bounds = new(4.5f, 4.5f);

		public Vector2 Bounds => bounds;

		private void OnDrawGizmos()
		{
			Gizmos.DrawWireCube(transform.position, new Vector3(bounds.x * 2, 3, bounds.y * 2));
		}
	}
}