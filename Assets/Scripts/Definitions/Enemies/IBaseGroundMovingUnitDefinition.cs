namespace Definitions.Enemies
{
	public interface IBaseGroundMovingUnitDefinition
	{
		public float HealthPoints { get; }
		public float MovementSpeed { get; }
		public float RotationSpeed { get; }
		public float DamageReductionMultiplier { get; }
	}
}