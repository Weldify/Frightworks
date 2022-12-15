using Sandbox;

namespace Frightworks;

public partial class SurvivorPlayer : BasePlayer
{
	public override void Spawn()
	{
		base.Spawn();

		SetModel( "models/citizen/citizen.vmdl" );

		InitFlashlight();
	}

	public override void ClientSpawn()
	{
		InitFlashlightClient();
	}

	public override void Simulate( IClient cl )
	{
		base.Simulate( cl );

		SimulateFlashlight();
	}

	public override void FrameSimulate( IClient cl )
	{
		base.FrameSimulate( cl );

		FrameSimulateFlashlight();
	}
}
