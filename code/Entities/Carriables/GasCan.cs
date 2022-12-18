using Sandbox;

namespace Frightworks;

public partial class GasCan : Carriable
{
	public override Model WorldModel => Model.Load( "models/gas_can/gas_can.vmdl" );
	public override Model ViewModel => Model.Load( "models/gas_can/gas_can.vmdl" );

	public float FuelRemaining = 0.4f;

	// Try to siphon this much fuel. Returns how much we actually siphoned
	public float Siphon( float amount )
	{
		var newRemaining = MathF.Max( FuelRemaining - amount, 0f );
		var change = FuelRemaining - newRemaining;

		FuelRemaining = newRemaining;

		return change;
	}

	public override bool CanCarry()
	{
		return Parent is not Generator
			&& FuelRemaining > 0f;
	}

	// If the gas can is empty, delete it after a short period of time
	public void TryRemove()
	{
		if ( FuelRemaining != 0f || Owner.IsValid() ) return;

		DeleteAsync( 5f );
	}

	public override bool OnUse( Entity ent )
	{
		// If we can carry, start carrying
		if ( CanCarry() )
		{
			return base.OnUse( ent );
		}

		// if we are parented to a generator, try fuelling it
		if ( Parent is Generator gen )
		{
			return gen.TryTickFuel();
		}

		return false;
	}

	public override void SimulateAnimator( CitizenAnimationHelper anim )
	{
		anim.HoldType = CitizenAnimationHelper.HoldTypes.HoldItem;
		anim.Handedness = CitizenAnimationHelper.Hand.Both;
		Survivor.SetAnimParameter( "holdtype_pose", 2.28f );
		anim.AimBodyWeight = 0.5f;
	}

	protected override void CreateViewModel()
	{
		ViewModelEntity = new ViewModel
		{
			Model = ViewModel,
			PositionOffset = new Vector3( 50f, 0f, -20f ),
		};
	}
}
