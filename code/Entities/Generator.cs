using Sandbox;

namespace Frightworks;

public partial class Generator : ModelEntity
{
	public override void Spawn()
	{
		SetModel( "models/generator/generator.vmdl" );
		SetupPhysicsFromModel( PhysicsMotionType.Static );

		Tags.Add( "solid" );
	}
}