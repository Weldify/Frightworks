global using Sandbox;
global using System;
global using System.Linq;
global using System.Collections.Generic;

namespace Frightworks;

public partial class FrightworksGame : GameManager
{
	public new static FrightworksGame Current;

	public FrightworksGame()
	{
		Current = this;

		if ( Game.IsClient )
			_ = new UI.Hud();
	}

	public override void Spawn()
	{
		var isLobbyMap = Game.Server.MapIdent == GameSettings.LobbyMapIdent;

		Behavior = isLobbyMap switch
		{
			true => new LobbyBehavior(),
			false => new MatchBehavior(),
		};
	}

	public override void ClientJoined( IClient cl )
	{
		base.ClientJoined( cl );

		Behavior.ClientJoined( cl );
	}

	[Event.Tick.Server]
	public void OnTickServer()
	{
		Behavior?.Tick();
	}
}
