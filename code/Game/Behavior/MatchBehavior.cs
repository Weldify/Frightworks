using Sandbox;

namespace Frightworks;

public partial class MatchBehavior : GameBehavior
{
	public bool HasSlasher;
	public long SlasherSteamId;

	public override void Activate()
	{
		// Deserialize transfer data
		var transferInfo = FileSystem.Data.ReadJson<MatchTransferInfo>( GameSettings.MatchTransferFilename );

		HasSlasher = transferInfo.HasSlasher;
		SlasherSteamId = transferInfo.SlasherSteamId;

		Log.Info( SlasherSteamId );
	}
}
