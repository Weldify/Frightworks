using Sandbox;

namespace Frightworks;

public partial class FirstPersonCamera : CameraController
{
	public float FieldOfView { get; set; } = 70f;

	public override void Update()
	{
		Camera.Main.FirstPersonViewer = Entity;

		Camera.Main.Rotation = Entity.ViewAngles.ToRotation();
		Camera.Main.Position = Entity.AimRay.Position;

		Camera.Main.FieldOfView = Screen.CreateVerticalFieldOfView( FieldOfView );
	}
}
