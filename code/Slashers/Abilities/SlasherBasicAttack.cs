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

		var tr = Trace.Ray( Slasher.AimRay.Position, Slasher.AimRay.Forward * 100f )
			.UseHitboxes()
			.WithAnyTags( "solid", "player" )
			.Ignore( Slasher )
			.Run();

		if ( tr.Entity is not SurvivorPlayer survivor ) return;

		survivor.TakeDamage( DamageInfo.Generic( 80f ) );
	}
}
