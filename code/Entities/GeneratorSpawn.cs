using Sandbox;
using Editor;

namespace Frightworks;

[Library( "fw_generator_spawn" ), HammerEntity]
[Title( "Generator Spawn" ), Category( "Objective" )]
[EditorModel( "models/generator/generator.vmdl" )]
public partial class GeneratorSpawn : Entity
{
	[Property( Title = "Generator Id" )]
	public int GeneratorId { get; set; } = 0;

	public override void Spawn()
	{
		Transmit = TransmitType.Never;
	}
}
