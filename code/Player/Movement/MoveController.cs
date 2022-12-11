using Sandbox;

namespace Frightworks;

public partial class MoveController : EntityComponent<Player>, ISingletonComponent
{
	public virtual void Simulate() { }
}
