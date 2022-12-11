using Sandbox;

namespace Frightworks;

public partial class Player
{
	public CameraController CameraController
	{
		get => Components.Get<CameraController>();
		set => Components.Add( value );
	}

	[Net, Predicted]
	public float EyeHeightScale { get; set; } = 1f;

	public override Ray AimRay => new( Position + Vector3.Up * 64f * Scale * EyeHeightScale, ViewAngles.Forward );

	void UpdateCamera()
	{
		CameraController?.Update();
	}
}
