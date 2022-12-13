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
			var exists = Game.Clients.Any( c => c.SteamId == steamId );
			if ( !exists ) return false;
		}

		return true;
	}

	void MakeStateOngoing()
	{
		State = MatchState.Ongoing;

		var survivorSpawns = Entity.All.OfType<SurvivorSpawn>();
		var slasherSpawns = Entity.All.OfType<SlasherSpawn>();

		var survivorSpawnId = 0;
		var slasherSpawnId = 0;

		foreach ( var (steamId, readyAs) in PlayerRoles )
		{
			var client = Game.Clients.First( c => c.SteamId == steamId );

			BasePlayer plr = readyAs switch
			{
				ReadyAs.Slasher => new SlasherPlayer(),
				ReadyAs.Survivor or _ => new SurvivorPlayer(),
			};
			client.Pawn = plr;

			if ( plr is SurvivorPlayer surv )
			{
				var spawn = survivorSpawns.ElementAt( survivorSpawnId );
				surv.Transform = spawn.Transform;
				surv.ResetInterpolation();

				survivorSpawnId = (survivorSpawnId + 1) % survivorSpawns.Count();
			}

			if ( plr is SlasherPlayer slasher )
			{
				var spawn = slasherSpawns.ElementAt( slasherSpawnId );
				slasher.Transform = spawn.Transform;
				slasher.ResetInterpolation();

				slasherSpawnId = (slasherSpawnId + 1) % slasherSpawns.Count();
			}
		}
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
