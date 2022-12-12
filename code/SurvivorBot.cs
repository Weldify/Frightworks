using Sandbox;

namespace Frightworks;

public partial class SurvivorBot : Bot
{
	[ConCmd.Admin( "fw_bot_survivor", Help = "Spawn a survivor bot" )]
	public static void SpawnSurvivorBot()
	{
		Game.AssertServer();

		_ = new SurvivorBot();
	}

	TimeSince timeSinceReady = 0f;

	void BuildLobbyInput( LobbyPlayer plr )
	{
		// Input.Pressed is spammed for some reason even if the key is just down
		Input.SetButton( InputButton.Slot1, false );

		if ( timeSinceReady > 1f && plr.ReadyAs != ReadyAs.Survivor )
		{
			timeSinceReady = 0f;
			Input.SetButton( InputButton.Slot1, true );
		}
	}

	public override void BuildInput()
	{
		var pawn = Client.Pawn;

		if ( pawn is LobbyPlayer plr )
		{
			BuildLobbyInput( plr );
			return;
		}
	}
}
