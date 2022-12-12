using Sandbox;

namespace Frightworks;

public partial class GameBehavior : BaseNetworkable
{
	public FrightworksGame GameManager;

	public virtual void Activate() { }
	public virtual void Update() { }
	public virtual BasePlayer CreatePlayerForClient( IClient cl ) 
	{
		return new BasePlayer();
	}
}
