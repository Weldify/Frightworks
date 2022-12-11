using Sandbox;

namespace Frightworks;

public partial class FirstPersonCamera : CameraController
{
	public override void Update()
	{
		Camera.FirstPersonViewer = Entity;

		Camera.Main.Rotation = Entity.ViewAngles.ToRotation();
		Camera.Main.Position = Entity.AimRay.Position;
	}
}
