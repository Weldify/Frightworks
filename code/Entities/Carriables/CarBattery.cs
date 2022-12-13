using Sandbox;

namespace Frightworks;

public partial class CarBattery : ModelEntity
{
	public override void Spawn()
	{
		SetModel("models/car_battery/car_battery.vmdl");
		SetupPhysicsFromModel( PhysicsMotionType.Dynamic );

		Tags.Add( "solid" );
	}
}
