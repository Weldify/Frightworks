namespace Frightworks;

public static class GameSettings
{
	public static bool IsDebug => true;

	public static float LobbyCountdownTime => IsDebug ? 2f : 10f;

	public static string MatchTransferFilename => "match_transfer_data.json";

	public static string LobbyMapIdent => "facepunch.flatgrass";
}
