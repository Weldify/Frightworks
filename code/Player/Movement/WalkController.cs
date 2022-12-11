using Sandbox;

namespace Frightworks;

public partial class WalkController : MoveController
{
	public override void Simulate()
	{
		Entity.Position += Entity.AimRay.Forward * 100f * Time.Delta;
	}
}
