using Sandbox;

namespace Frightworks;

public partial class FrightworksGame : GameManager
{
	public override void ClientJoined( IClient cl )
	{
		base.ClientJoined( cl );

		var plr = new Player();
		cl.Pawn = plr;
	}
}
