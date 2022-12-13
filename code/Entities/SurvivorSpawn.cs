using Sandbox;
using Editor;

namespace Frightworks;

[Library( "fw_survivor_spawn" ), HammerEntity]
[Title( "Survivor spawn" ), Category( "Player" )]
[EditorModel( "models/dev/playerstart_tint.vmdl", "green" )]
public partial class SurvivorSpawn : Entity
{
	public override void Spawn()
	{
		Transmit = TransmitType.Never;
	}
}
