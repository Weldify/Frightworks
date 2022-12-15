using Sandbox;

namespace Frightworks;

public enum ReadyAs
{
	Survivor,
	Slasher,
	None
}

public partial class LobbyPlayer : BasePlayer
{
	[Net]
	public ReadyAs ReadyAs { get; set; } = ReadyAs.None;
	public SlasherType SlasherType;

	[ConCmd.Server( "fw_ready_slasher" )]
	public static void ReadyAsSlasher( SlasherType type )
	{
		var caller = ConsoleSystem.Caller;
		if ( caller is null ) return;

		if ( caller.Pawn is not LobbyPlayer plr ) return;

		plr.SlasherType = type;
		plr.ReadyAs = ReadyAs.Slasher;
	}

	public override void Simulate( IClient cl )
	{
		base.Simulate( cl );

		if ( !Game.IsServer ) return;

		if ( Input.Pressed( InputButton.Slot1 ) )
		{
			ReadyAs = ReadyAs switch
			{
				ReadyAs.Survivor => ReadyAs.None,
				_ => ReadyAs.Survivor,
			};

			return;
		}
	}

	[Event.Tick.Client]
	public void OnClientTick()
	{
		if ( Input.Pressed( InputButton.Slot2 ) && ReadyAs == ReadyAs.None )
		{
			var selectionUI = UI.LobbySlasherSelection.Current;
			if ( selectionUI is null ) return;

			selectionUI.Enabled = !selectionUI.Enabled;
		}
	}
}
