using Frightworks;
using Sandbox;

namespace Frightworks;

public partial class SurvivorBot : FrightworksBot
{
	[ConCmd.Admin( "fw_bot_survivor", Help = "Spawn a survivor bot" )]
	public void SpawnSurvivorBot()
	{
		Game.AssertServer();

		_ = new SurvivorBot();
	}

	TimeSince timeSinceReady = 0f;

	public override void BuildLobbyInput( LobbyPlayer plr )
	{
		// Input.Pressed is spammed for some reason even if the key is just down
		Input.SetButton( InputButton.Slot1, false );

		if ( timeSinceReady > 1f && plr.ReadyAs != ReadyAs.Survivor )
		{
			timeSinceReady = 0f;
			Input.SetButton( InputButton.Slot1, true );
		}
	}
}
