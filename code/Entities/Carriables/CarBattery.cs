using Sandbox;

namespace Frightworks;

public partial class CarBattery : Carriable
{
	public override Model WorldModel => Model.Load( "models/car_battery/car_battery.vmdl" );
	public override Model ViewModel => Model.Load( "models/car_battery/car_battery.vmdl" );

	public override bool CanCarry()
	{
		return Parent is not Generator;
	}

	public override void SimulateAnimator( CitizenAnimationHelper anim )
	{
		anim.HoldType = CitizenAnimationHelper.HoldTypes.HoldItem;
		anim.Handedness = CitizenAnimationHelper.Hand.Right;
		anim.AimBodyWeight = 0.5f;
	}

	protected override void CreateViewModel()
	{
		ViewModelEntity = new ViewModel
		{
			Model = ViewModel,
			PositionOffset = new Vector3( 50f, 0f, -30f ),
		};
	}
}
