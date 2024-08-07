using UnityEngine;

namespace Definitions.Enemies
{
	public abstract class UnitDefinition : ScriptableObject
	{
		[Header("Unit view")]
		[SerializeField]
		private GameObject unitView;

		public GameObject UnitView => unitView;
	}
}