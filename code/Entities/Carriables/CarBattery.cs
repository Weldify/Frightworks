using Sandbox;

namespace Frightworks;

public partial class CarBattery : Carriable
{
	public override Model WorldModel => Model.Load( "models/car_battery/car_battery.vmdl" );

	public override void SimulateAnimator( CitizenAnimationHelper anim )
	{
		anim.HoldType = CitizenAnimationHelper.HoldTypes.HoldItem;
		anim.Handedness = CitizenAnimationHelper.Hand.Right;
		anim.AimBodyWeight = 0.5f;
	}
}
