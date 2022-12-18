using Editor;

namespace Frightworks;

[Library( "fw_helicopter_spawn" ), HammerEntity]
[Title( "Helicopter Spawn" ), Category( "Objective" )]
[EditorModel( "models/helicopter/helicopter.vmdl" )]
public partial class HelicopterSpawn : Entity
{
	public override void Spawn()
	{
		Transmit = TransmitType.Never;
	}
}
