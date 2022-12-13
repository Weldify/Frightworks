using Sandbox;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Frightworks;

public partial class MatchBehavior : GameBehavior
{
	[Net]
	public IDictionary<long, ReadyAs> PlayerRoles { get; set; }

	public override void Activate()
	{
		// Deserialize transfer data
		var transferInfo = FileSystem.Data.ReadJson<MatchTransferInfo>( GameSettings.MatchTransferFilename );

		PlayerRoles = transferInfo.PlayerRoles;

		// Bots are steamids smaller than 0
		var botPlayerRoles = PlayerRoles.Where( ( k, v ) => v < 0 );

		foreach ( (var steamid, var readyAs) in botPlayerRoles )
		{
			FrightworksBot bot = readyAs switch
			{
				ReadyAs.Slasher => new SlasherBot(),
				ReadyAs.Survivor or _ => new SurvivorBot(),
			};

			// Inject the bot's steamid into the player roles
			PlayerRoles.Remove( steamid );
			PlayerRoles.Add( bot.Client.SteamId, readyAs );
		}
	}
}
