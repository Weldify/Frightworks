using Sandbox;
using System;
using System.Linq;
using System.Text.Json;

namespace Frightworks;

public partial class LobbyBehavior : GameBehavior
{
	[Net]
	public TimeUntil TimeUntilStart { get; set; } = GameSettings.LobbyCountdownTime;

	public override void ClientJoined( IClient cl )
	{
		base.ClientJoined( cl );

		var plr = new LobbyPlayer();
		cl.Pawn = plr;

		MoveToSpawnpoint( plr );

		plr.Respawn();
	}

	public override void Tick()
	{
		if ( !CanStart() )
		{
			TimeUntilStart = GameSettings.LobbyCountdownTime;
			return;
		}

		if ( TimeUntilStart > 0f ) return;

		ReduceSlashers();

		var transferInfo = new MatchTransferInfo();

		var relevantPlayers = Entity.All.OfType<LobbyPlayer>()
			.Where( p => p.ReadyAs != ReadyAs.None );

		var botCount = 0;
		foreach ( var plr in relevantPlayers )
		{
			if ( plr.ReadyAs == ReadyAs.Slasher )
				transferInfo.SlasherType = plr.SlasherType;

			// Bots can't get sent to the new map
			// So we fake it by creating them on the other side
			if ( plr.Client.IsBot )
			{
				botCount++;
				// Negative id lets us know its a bot
				transferInfo.PlayerRoles.Add( -botCount, plr.ReadyAs );
				continue;
			}

			transferInfo.PlayerRoles.Add( plr.Client.SteamId, plr.ReadyAs );
		}

		FileSystem.Data.WriteJson( GameSettings.MatchTransferFilename, transferInfo );

		Game.ChangeLevel( GetRandomMatchMapIdent() );
	}

	bool CanStart()
	{
		var allPlayers = Entity.All.OfType<LobbyPlayer>();
		var plrCount = allPlayers.Count();

		if ( plrCount == 0 )
			return false;
		if ( !GameSettings.IsDebug && plrCount < 2 )
			return false;

		var readyPlayers = allPlayers.Where( p => p.ReadyAs != ReadyAs.None );

		var slasherPlayers = readyPlayers.Where( p => p.ReadyAs == ReadyAs.Slasher );
		if ( !GameSettings.IsDebug && !slasherPlayers.Any() )
			return false;

		return allPlayers.Count() == readyPlayers.Count();
	}

	// Make sure there is only one player ready as slasher
	void ReduceSlashers()
	{
		var slashers = Entity.All.OfType<LobbyPlayer>()
			.Where( p => p.ReadyAs == ReadyAs.Slasher )
			.OrderBy( _ => Guid.NewGuid() )
			.ToList();

		foreach ( var slasher in slashers )
			slasher.ReadyAs = ReadyAs.Survivor;

		if ( slashers.Any() )
			slashers.First().ReadyAs = ReadyAs.Slasher;
	}

	static string GetRandomMatchMapIdent()
	{
		return GameSettings.MatchMapIdents
			.Split( ";" )
			.OrderBy( _ => Guid.NewGuid() )
			.First();
	}

	void MoveToSpawnpoint( LobbyPlayer plr )
	{
		var spawnPoints = Entity.All.OfType<SpawnPoint>()
			.OrderBy( x => Guid.NewGuid() );

		if ( !spawnPoints.Any() ) return;

		plr.Transform = spawnPoints.First().Transform;
		plr.ResetInterpolation();
	}
}
