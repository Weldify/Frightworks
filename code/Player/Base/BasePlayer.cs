using Sandbox;
using System;
using System.Linq;

namespace Frightworks;

// Generic player class all Frightworks characters derive from
public partial class BasePlayer : AnimatedEntity
{

	[ClientInput]
	public Angles ViewAngles { get; set; }

	void CreateHull()
	{
		SetupPhysicsFromAABB( PhysicsMotionType.Keyframed, new Vector3( -16, -16, 0 ), new Vector3( 16, 16, 72 ) );

		EnableHitboxes = true;
	}

	public override void Spawn()
	{
		base.Spawn();

		Tags.Add( "player" );

		EnableLagCompensation = true;

		EnableHideInFirstPerson = true;
		EnableShadowInFirstPerson = true;
		EnableDrawing = false;

		CreateHull();

		LifeState = LifeState.Dead;
	}

	public virtual void Respawn()
	{
		EnableDrawing = true;
		EnableAllCollisions = true;

		LifeState = LifeState.Alive;
		Health = 100f;
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

		DoUse();

		DoMovement();
		DoAnimation();
	}

	public override void FrameSimulate( IClient cl )
	{
		UpdateCamera();
	}

	DamageInfo lastDamage;
	public override void TakeDamage( DamageInfo info )
	{
		lastDamage = info;
		base.TakeDamage( info );
	}

	public override void OnKilled()
	{
		EnableDrawing = false;
		EnableAllCollisions = false;

		var rag = new Ragdoll();
		rag.Init( this, lastDamage.Force );

		LifeState = LifeState.Dead;
	}
}
