namespace Frightworks;

public static class GameSettings
{
	public static bool IsDebug => true;

	public static float LobbyCountdownTime => IsDebug ? 2f : 10f;
	public static string LobbyMapIdent => "facepunch.flatgrass";
	public static string MatchTransferFilename => "match_transfer_data.json";

	// How long do we wait before starting the match once everyone is in
	public static float MatchPreWaitTime => IsDebug ? 3f : 10f;

	public static int GeneratorCount => 3;
	public static int PoweredGeneratorsNeeded => 2;
	public static int GasCansPerGenerator => 4;
}
