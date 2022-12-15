using Sandbox;
using Sandbox.Internal;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frightworks;

public partial class MatchBehavior : GameBehavior
{

	[Net]
	public IDictionary<long, ReadyAs> PlayerRoles { get; set; }
	public SlasherType SlasherType { get; set; }

	public override void Activate()
	{
		// Deserialize transfer data
		var transferInfo = FileSystem.Data.ReadJson<MatchTransferInfo>( GameSettings.MatchTransferFilename );

		PlayerRoles = transferInfo.PlayerRoles;
		SlasherType = transferInfo.SlasherType;
	}

	void ReplaceBotRoles()
	{
		// Bots are steamids smaller than 0
		var botPlayerRoles = new Dictionary<long, ReadyAs>( PlayerRoles.Where( p => p.Key < 0 ) );

		foreach ( var (steamid, readyAs) in botPlayerRoles )
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

	bool firstUpdate = true;

	public override void Update()
	{
		if ( firstUpdate )
		{
			firstUpdate = false;
			ReplaceBotRoles();
		}

		UpdateState();
	}
}
