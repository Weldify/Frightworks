using Sandbox;

namespace Frightworks;

public partial class GasCan : ModelEntity
{
	public override void Spawn()
	{
		SetModel( "models/gas_can/gas_can.vmdl" );
		SetupPhysicsFromModel( PhysicsMotionType.Dynamic );

		Tags.Add( "solid" );
	}
}
