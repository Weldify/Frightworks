﻿using Sandbox;
using System;
using System.Linq;
using System.Text.Json;

namespace Frightworks;

public partial class LobbyBehavior : GameBehavior
{
	[Net]
	public TimeUntil TimeUntilStart { get; set; } = GameSettings.LobbyCountdownTime;

	public override BasePlayer CreatePlayerForClient( IClient cl )
	{
		return new LobbyPlayer();
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
			.Where( p => p.ReadyAs == ReadyAs.Slasher );

		var remaining = slashers
			.OrderBy( p => Guid.NewGuid() )
			.First();

		foreach ( var slasher in slashers )
			slasher.ReadyAs = ReadyAs.Survivor;

		remaining.ReadyAs = ReadyAs.Slasher;
	}

	public override void Update()
	{
		if ( !CanStart() )
		{
			TimeUntilStart = GameSettings.LobbyCountdownTime;
			return;
		}

		if ( TimeUntilStart > 0f ) return;

		var transferInfo = new MatchTransferInfo();

		ReduceSlashers();

		var relevantPlayers = Entity.All.OfType<LobbyPlayer>()
			.Where( p => p.ReadyAs != ReadyAs.None );

		foreach ( var plr in relevantPlayers )
		{
			transferInfo.PlayerRoles.Add( plr.Client.SteamId, plr.ReadyAs );
		}

		FileSystem.Data.WriteJson( GameSettings.MatchTransferFilename, transferInfo );

		Game.ChangeLevel( "local.fw_test_map#local" );
	}
}