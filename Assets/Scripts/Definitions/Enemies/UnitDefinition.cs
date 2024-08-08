using UnityEngine;

namespace Definitions.Enemies
{
	public abstract class UnitDefinition : ScriptableObject, IUnitDefinition
	{
		[Header("Unit view")]
		[SerializeField]
		private GameObject unitView;

		public GameObject UnitView => unitView;
	}
}