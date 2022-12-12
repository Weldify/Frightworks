using Sandbox;

namespace Frightworks;

public partial class CameraController : EntityComponent<BasePlayer>, ISingletonComponent
{
	/// <summary>
	/// Runs in FrameSimulate to update the camera.
	/// </summary>
	public virtual void Update() { }
}
