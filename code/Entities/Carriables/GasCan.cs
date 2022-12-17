using Sandbox;

namespace Frightworks;

public partial class GasCan : Carriable
{
	public override Model WorldModel => Model.Load( "models/gas_can/gas_can.vmdl" );
	public override Model ViewModel => Model.Load( "models/gas_can/gas_can.vmdl" );

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
