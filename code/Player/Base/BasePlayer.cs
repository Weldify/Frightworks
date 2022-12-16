using Sandbox;
using System;
using System.Linq;

namespace Frightworks;

// Generic player class all Frightworks characters derive from
public partial class BasePlayer : AnimatedEntity
{

	[ClientInput]
	public Angles ViewAngles { get; set; }

	public override void Spawn()
	{
		base.Spawn();

		EnableLagCompensation = true;

		EnableHideInFirstPerson = true;
		EnableShadowInFirstPerson = true;

		// Once implemented, move the code below into Respawn

		CameraController = new ThirdPersonCamera();
		MoveController = new WalkController();
		AnimationController = new HumanoidAnimator();

		CreateHull();
	}

	void CreateHull()
	{
		SetupPhysicsFromAABB( PhysicsMotionType.Keyframed, new Vector3( -16, -16, 0 ), new Vector3( 16, 16, 72 ) );

		EnableHitboxes = true;
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
	}

	public override void FrameSimulate( IClient cl )
	{
		UpdateCamera();
	}
}
