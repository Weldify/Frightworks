using Sandbox;
using System;
using System.Linq;
using Frightworks;

namespace Frightworks.UI;

public partial class LobbyPlayers
{
	protected override int BuildHash()
	{
		var steamIds = Game.Clients.HashCombine( x => x.SteamId );
		var ready = Entity.All.OfType<LobbyPlayer>().HashCombine( x => Convert.ToByte( x.ReadyAs ) );

		return HashCode.Combine( steamIds, ready );
	}
}
