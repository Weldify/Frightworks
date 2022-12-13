using Sandbox;
using Editor;

namespace Frightworks;

[Library( "fw_slasher_spawn" ), HammerEntity]
[Title( "Slasher spawn" ), Category( "Player" )]
[EditorModel( "models/dev/playerstart_tint.vmdl", "red" )]
public partial class SlasherSpawn : Entity
{
	public override void Spawn()
	{
		Transmit = TransmitType.Never;
	}
}
