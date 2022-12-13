using Sandbox;
using System.Linq;

namespace Frightworks;

public enum MatchState
{
	WaitingForPlayers,
	Ongoing,
	Ended,
}

public partial class MatchBehavior
{
	[Net]
	public MatchState State { get; set; } = MatchState.WaitingForPlayers;

	bool CanStartRound()
	{
		foreach ( (var steamId, var _) in PlayerRoles )
		{
			var exists = Game.Clients.Where( c => c.SteamId == steamId ).Any();
			if ( !exists ) return false;
		}

		return true;
	}

	void MakeStateOngoing()
	{
		State = MatchState.Ongoing;

		Log.Info( "game started wah" );
	}

	public void UpdateState()
	{
		// Weird ass state machine
		while ( true )
		{
			switch ( State )
			{
				case MatchState.WaitingForPlayers:
					if ( CanStartRound() )
					{
						MakeStateOngoing();
						continue;
					}

					break;
			}

			break;
		}
	}
}
