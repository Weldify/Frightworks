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

	public override void Spawn()
	{
		Behavior = new LobbyBehavior();
	}

	public override void ClientJoined( IClient cl )
	{
		base.ClientJoined( cl );

		var plr = new BasePlayer();
		cl.Pawn = plr;
	}

	[Event.Tick.Server]
	public void OnTickServer()
	{
		Behavior?.Update();
	}
}
