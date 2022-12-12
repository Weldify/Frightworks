using Sandbox;

namespace Frightworks;

public partial class AnimationController : EntityComponent<BasePlayer>, ISingletonComponent
{
	public virtual void Simulate() { }
}
