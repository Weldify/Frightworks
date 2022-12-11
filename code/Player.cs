using Sandbox;

namespace Frightworks;

public partial class Player : AnimatedEntity
{
	public override void Spawn()
	{
		base.Spawn();

		Tags.Add( "player" );
		SetModel( "models/citizen/citizen.vmdl" );
	}
}
