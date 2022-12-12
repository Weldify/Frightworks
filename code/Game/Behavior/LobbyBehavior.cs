using Sandbox;
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

	public override void Update()
	{
		if ( !CanStart() )
		{
			TimeUntilStart = GameSettings.LobbyCountdownTime;
			return;
		}

		if ( TimeUntilStart > 0f ) return;

		var transferInfo = new MatchTransferInfo();

		var slashers = Entity.All.OfType<LobbyPlayer>()
			.Where( p => p.ReadyAs == ReadyAs.Slasher );

		if ( slashers.Any() )
		{
			var slasher = slashers
				.OrderBy( p => Guid.NewGuid() )
				.First();

			transferInfo.HasSlasher = true;
			transferInfo.SlasherSteamId = slasher.Client.SteamId;
		}

		FileSystem.Data.WriteJson( GameSettings.MatchTransferFilename, transferInfo );

		Game.ChangeLevel( "local.fw_test_map#local" );
	}
}
