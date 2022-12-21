using Sandbox;

namespace Frightworks;

public partial class SurvivorPlayer : BasePlayer
{
	public bool IsInHelicopter = false;

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

	public override void Respawn()
	{
		base.Respawn();

		AnimationController = new HumanoidAnimator();
		CameraController = new FirstPersonCamera();
		MoveController = new WalkController
		{
			DefaultSpeed = GameSettings.SurvivorWalkSpeed,
			WalkSpeed = GameSettings.SurvivorWalkSpeed,
			SprintSpeed = GameSettings.SurvivorSprintSpeed,
		};
	}

	[Event.Tick.Client]
	public void OnClientTick()
	{
		TickTerrorSound();
	}

	public override void Simulate( IClient cl )
	{
		base.Simulate( cl );

		SimulateFlashlight();
		SimulateCarriable();
	}

	public override void FrameSimulate( IClient cl )
	{
		base.FrameSimulate( cl );

		FrameSimulateFlashlight();
	}

	public void TrySitInHelicopter( Helicopter heli )
	{
		if ( IsInHelicopter ) return;
		if ( LifeState != LifeState.Alive ) return;
		IsInHelicopter = true;

		SetParent( heli );
		EnableAllCollisions = false;

		MoveController = null;
		Transform = heli.Transform;
	}

	public override float FootstepVolume()
	{
		if ( MoveController is not WalkController wc ) return 0f;
		return wc.Sprinting ? 1.5f : 0f;
	}
}
