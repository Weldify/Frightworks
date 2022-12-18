namespace Frightworks;

public partial class Helicopter : ModelEntity, IUse
{
	public override void Spawn()
	{
		base.Spawn();

		SetModel( "models/helicopter/helicopter.vmdl" );
		SetupPhysicsFromModel( PhysicsMotionType.Keyframed );

		Tags.Add( "solid" );

		PlaySound( "helicopter.loop" );

		EnableTouch = true;
	}

	public bool IsUsable( Entity ent )
	{
		return ent is SurvivorPlayer plr
			&& !plr.IsInHelicopter;
	}

	public bool OnUse(Entity ent)
	{
		if ( ent is not SurvivorPlayer survivor ) return false;

		survivor.TrySitInHelicopter( this );

		return false;
	}
}
