using Sandbox;

namespace Frightworks;

public partial class FrightworksBot : Bot
{
	public virtual void BuildLobbyInput( LobbyPlayer plr ) { }
	public virtual void BuildMatchInput( BasePlayer plr ) { }

	public override void BuildInput()
	{
		var pawn = Client.Pawn;
		var gameBehavior = FrightworksGame.Current.Behavior;

		if ( gameBehavior is LobbyBehavior && pawn is LobbyPlayer lobbyPlayer )
		{
			BuildLobbyInput( lobbyPlayer );
			return;
		}

		if ( gameBehavior is MatchBehavior && pawn is BasePlayer basePlayer )
			BuildMatchInput( basePlayer );
	}
}
