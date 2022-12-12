using Sandbox;

namespace Frightworks;

public partial class MatchBehavior : GameBehavior
{

	public override void Activate()
	{
		// Deserialize transfer data
		var transferInfo = FileSystem.Data.ReadJson<MatchTransferInfo>( GameSettings.MatchTransferFilename );
	}
}
