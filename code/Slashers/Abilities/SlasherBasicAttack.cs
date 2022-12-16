namespace Frightworks;

public partial class SlasherBasicAttack : SlasherAbility
{
	public override bool CanUse()
	{
		return Input.Pressed( InputButton.PrimaryAttack );
	}

	public override void SimulateUse()
	{
		if ( !Prediction.FirstTime ) return;

		var _ = LagCompensation();

		var tracePos = Slasher.AimRay.Position;
		var tr = Trace.Ray( tracePos, tracePos + Slasher.AimRay.Forward * 100f )
			.UseHitboxes()
			.WithAnyTags( "solid", "player" )
			.Ignore( Slasher )
			.Run();
		
		if ( !Game.IsServer ) return;
		if ( !tr.Hit ) return;
		if ( tr.Entity is not SurvivorPlayer survivor ) return;

		tr.Surface.DoBulletImpact( tr );
		survivor.TakeDamage( DamageInfo.Generic( 80f ) );
	}
}
