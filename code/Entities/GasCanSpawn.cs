using Editor;
using Sandbox;

namespace Frightworks;

[Library( "fw_gas_can_spawn" ), HammerEntity]
[Title( "Gas Can Spawn" ), Category( "Objective" )]
[EditorModel( "models/gas_can/gas_can.vmdl" )]
public partial class GasCanSpawn : Entity
{
	public override void Spawn()
	{
		Transmit = TransmitType.Never;
	}
}
