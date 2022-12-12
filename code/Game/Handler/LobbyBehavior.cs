using Sandbox;

namespace Frightworks;

public partial class LobbyBehavior : GameBehavior
{
	public override void Activate()
	{
		Log.Info( "Heyea" );
	}

	public override BasePlayer CreatePlayerForClient( IClient cl )
	{
		return new LobbyPlayer();
	}
}
