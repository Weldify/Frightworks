using Sandbox;
using System;
using System.Linq;

namespace Frightworks;

// Generic player class all Frightworks characters derive from
public partial class Player : AnimatedEntity
{
	[Net]
	public bool IsReady { get; set; } = false;

	[ClientInput]
	public Angles ViewAngles { get; set; }

	public override void Spawn()
	{
		base.Spawn();

		EnableLagCompensation = true;

		EnableHideInFirstPerson = true;
		EnableShadowInFirstPerson = true;

		SetModel( "models/citizen/citizen.vmdl" );

		// Once implemented, move the code below into Respawn

		CameraController = new ThirdPersonCamera();
		MoveController = new WalkController();
		AnimationController = new HumanoidAnimator();

		CreateHull();

		MoveToSpawnpoint();
	}

	void CreateHull()
	{
		SetupPhysicsFromAABB( PhysicsMotionType.Keyframed, new Vector3( -16, -16, 0 ), new Vector3( 16, 16, 72 ) );

		EnableHitboxes = true;
	}

	// Temporary until survivor spawn system is implemented
	void MoveToSpawnpoint()
	{
		var spawnPoints = All.OfType<SpawnPoint>();
		if ( !spawnPoints.Any() ) return;

		var first = spawnPoints
			.OrderBy( x => Guid.NewGuid() )
			.First();

		Transform = first.Transform;
		ResetInterpolation();
	}

	public override void BuildInput()
	{
		var view = ViewAngles;
		view += Input.AnalogLook;
		view.pitch = view.pitch.Clamp( -89, 89f );
		view.roll = 0f;
		ViewAngles = view.Normal;

		base.BuildInput();
	}

	public override void Simulate( IClient cl )
	{
		if ( Prediction.FirstTime && Input.Pressed( InputButton.View ) )
		{
			CameraController = CameraController switch
			{
				FirstPersonCamera => new ThirdPersonCamera(),
				ThirdPersonCamera or _ => new FirstPersonCamera()
			};
		}

		DoMovement();
		DoAnimation();

		if ( Game.IsServer && Input.Pressed( InputButton.Slot0 ) )
		{
			IsReady = !IsReady;
		}
	}

	public override void FrameSimulate( IClient cl )
	{
		UpdateCamera();
	}
}
