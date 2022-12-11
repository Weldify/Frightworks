using Sandbox;

namespace Sandbox.Player.Camera;

public partial class ThirdPersonCamera : CameraController
{
	public override void Update()
	{
		Camera.FirstPersonViewer = null;

		var rotation = Entity.ViewAngles.ToRotation();
		var eyePos = Entity.AimRay.Position;

		// Camera collision
		var tr = Trace.Ray( eyePos, eyePos - rotation.Forward * 130f )
			.WithAnyTags( "solid" )
			.Ignore( Entity )
			.Radius( 8f )
			.Run();

		Camera.Rotation = rotation;
		Camera.Position = tr.EndPosition;
	}
}
