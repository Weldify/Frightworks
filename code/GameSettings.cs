namespace Frightworks;

public static class GameSettings
{
	public static string LobbyMapIdent => "tgm.fw_lobby";
	[ConVar.Server( "fw_map_idents" )]
	static string userMatchMapIdents { get; set; }
	public static string MatchMapIdents => userMatchMapIdents;

	// Do we want to run debug mode in the editor?
	[ConVar.Replicated( "fw_debug" )]
	public static bool IsDebug { get; set; }

	// How long it takes for the game to start once everyone is ready
	public static float LobbyCountdownTime => IsDebug ? 2f : 10f;
	public static string MatchTransferFilename => "match_transfer_data.json";
	// How long do we wait before starting the match once everyone is in
	public static float MatchPreWaitTime => IsDebug ? 3f : 10f;

	// How many generators to spawn on the map
	public static int GeneratorCount => 3;
	// How many generators need to be powered for the helicopter to arrive
	public static int PoweredGeneratorsNeeded => 2;
	// How many gas cans should spawn per every generator on the map
	public static int GasCansPerGenerator => 5;
	// How fast the fuel is transferred from a gas can into a generator
	public static float GasCanPourSpeed => IsDebug ? 0.2f : 0.03f;
	// How fast the helicopter arrives after enough generators have started
	public static float HelicopterArriveTime => IsDebug ? 5f : 30f;

	public static float SurvivorSprintSpeed => 230f;
	public static float SurvivorWalkSpeed => SurvivorSprintSpeed * 0.6f;

	public static float DefaultTerrorRadius => 700f;
}
