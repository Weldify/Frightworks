using Sandbox;

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
		Behavior = new LobbyBehavior();
	}

	public override void ClientJoined( IClient cl )
	{
		base.ClientJoined( cl );

		var plr = Behavior.CreatePlayerForClient( cl );
		cl.Pawn = plr;
	}

	[Event.Tick.Server]
	public void OnTickServer()
	{
		Behavior?.Update();
	}
}
