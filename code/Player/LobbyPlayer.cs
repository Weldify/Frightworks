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

	public override void Simulate( IClient cl )
	{
		base.Simulate( cl );

		if ( !Game.IsServer ) return;

		if ( Input.Pressed( InputButton.Slot1 ) )
		{
			Log.Info( "prsed" );
			ReadyAs = ReadyAs switch
			{
				ReadyAs.Survivor => ReadyAs.None,
				_ => ReadyAs.Survivor,
			};

			return;
		}

		if ( Input.Pressed( InputButton.Slot2 ) )
		{
			ReadyAs = ReadyAs switch
			{
				ReadyAs.Slasher => ReadyAs.None,
				_ => ReadyAs.Slasher,
			};

			return;
		}
	}
}
