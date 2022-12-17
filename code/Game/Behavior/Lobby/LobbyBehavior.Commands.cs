namespace Frightworks;

public partial class LobbyBehavior
{
	[ConCmd.Admin( "fw_test_survivor" )]
	public static void RunSurvivorTest()
	{
		var transferInfo = new MatchTransferInfo
		{
			PlayerRoles = new Dictionary<long, ReadyAs>
			{
				{ConsoleSystem.Caller.SteamId, ReadyAs.Survivor},
				{-1, ReadyAs.Slasher},
			},
			SlasherType = SlasherType.Terrance,
		};

		FileSystem.Data.WriteJson( GameSettings.MatchTransferFilename, transferInfo );

		Game.ChangeLevel( "local.fw_test_map#local" );
	}

	[ConCmd.Admin( "fw_test_slasher" )]
	public static void RunSlasherTest()
	{
		var transferInfo = new MatchTransferInfo
		{
			PlayerRoles = new Dictionary<long, ReadyAs>
			{
				{ConsoleSystem.Caller.SteamId, ReadyAs.Slasher},
				{-1, ReadyAs.Survivor},
			},
			SlasherType = SlasherType.Terrance,
		};

		FileSystem.Data.WriteJson( GameSettings.MatchTransferFilename, transferInfo );

		Game.ChangeLevel( "local.fw_test_map#local" );
	}
}
