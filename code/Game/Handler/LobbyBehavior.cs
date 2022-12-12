using Sandbox;
using System.Linq;

namespace Frightworks;

public partial class LobbyBehavior : GameBehavior
{
	[Net]
	public TimeUntil TimeUntilStart { get; set; } = GameSettings.LobbyCountdownTime;

	public override BasePlayer CreatePlayerForClient( IClient cl )
	{
		return new LobbyPlayer();
	}

	bool IsEveryoneReady()
	{
		var allPlayers = Entity.All.OfType<LobbyPlayer>();
		var plrCount = allPlayers.Count();

		if ( plrCount == 0 )
			return false;
		if ( !GameSettings.IsDebug && plrCount < 2 )
			return false;

		var readyPlayers = allPlayers.Where( p => p.ReadyAs != ReadyAs.None );

		return allPlayers.Count() == readyPlayers.Count();
	}

	public override void Update()
	{
		if ( !IsEveryoneReady() )
		{
			TimeUntilStart = GameSettings.LobbyCountdownTime;
			return;
		}
	}
}
