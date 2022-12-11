using Sandbox;

namespace Frightworks;

// Generic player class all Frightworks characters derive from
public partial class Player : AnimatedEntity
{
	[ClientInput]
	public Angles ViewAngles { get; set; }

	public override Ray AimRay => new( Position + Vector3.Up * 64f, ViewAngles.Forward );

	public override void Spawn()
	{
		base.Spawn();

		EnableHideInFirstPerson = true;
		EnableShadowInFirstPerson = true;

		SetModel( "models/citizen/citizen.vmdl" );

		CameraController = new ThirdPersonCamera();
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
		if ( Prediction.FirstTime && Input.Pressed(InputButton.View))
		{
			CameraController = CameraController switch
			{
				FirstPersonCamera => new ThirdPersonCamera(),
				ThirdPersonCamera or _ => new FirstPersonCamera()
			};
		}

		DoMovement();
	}

	public override void FrameSimulate( IClient cl )
	{
		UpdateCamera();
	}
}
