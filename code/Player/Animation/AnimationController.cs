using Sandbox;

namespace Frightworks;

public partial class AnimationController : EntityComponent<Player>, ISingletonComponent
{
	public virtual void Simulate() { }
}
