using Sandbox;

namespace Frightworks;

public partial class GameBehavior : BaseNetworkable
{
	public FrightworksGame GameManager;

	public virtual void Activate() { }
	public virtual void Tick() { }
	public virtual void ClientJoined( IClient cl ) { }
}
