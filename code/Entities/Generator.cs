using Editor;
using Sandbox;

namespace Frightworks;

[Library( "frightworks_generator" ), HammerEntity]
[Title( "Generator" )]
[EditorModel( "models/generator/generator.vmdl" )]
public partial class Generator : ModelEntity
{
	public override void Spawn()
	{
		base.Spawn();

		SetModel( "models/generator/generator.vmdl" );
	}
}
