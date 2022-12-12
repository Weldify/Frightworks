using Sandbox;

namespace Frightworks;

public partial class FrightworksGame : GameManager
{
	public new static FrightworksGame Current;

	[Net] public bool IsLobby { get; set; } = true;

	public FrightworksGame()
	{
		Current = this;

		if ( Game.IsClient )
			_ = new UI.Hud();
	}

	public override void ClientJoined( IClient cl )
	{
		base.ClientJoined( cl );

		var plr = new Player();
		cl.Pawn = plr;
	}

	[Event.Tick.Server]
	public void OnTickServer()
	{
		Behavior?.Update();
	}
}
