using Sandbox;

namespace Frightworks;

public partial class SlasherBot : FrightworksBot
{
	[ConCmd.Admin( "fw_bot_slasher", Help = "Spawn a slasher bot" )]
	public static void SpawnSurvivorBot()
	{
		Game.AssertServer();

		_ = new SlasherBot();
	}

	TimeSince timeSinceReady = 0f;

	public override void BuildLobbyInput( LobbyPlayer plr )
	{
		// Input.Pressed is spammed for some reason even if the key is just down
		Input.SetButton( InputButton.Slot2, false );

		if ( timeSinceReady > 1f && plr.ReadyAs != ReadyAs.Slasher )
		{
			timeSinceReady = 0f;
			Input.SetButton( InputButton.Slot2, true );
		}
	}
}
