using Sandbox;
using static Sandbox.Event;

namespace Frightworks;

public partial class HumanoidAnimator : AnimationController
{
	public override void Simulate()
	{
		var controller = Entity.MoveController;
		if ( controller is null ) return;

		// where should we be rotated to
		var turnSpeed = 0.02f;
		var inputRotation = Entity.ViewAngles.ToRotation();

		var idealRotation = Rotation.LookAt( inputRotation.Forward.WithZ( 0 ), Vector3.Up );
		Entity.Rotation = Rotation.Slerp( Entity.Rotation, idealRotation, controller.WishVelocity.Length * Time.Delta * turnSpeed );
		Entity.Rotation = Entity.Rotation.Clamp( idealRotation, 45f, out var shuffle ); // lock facing to within 45 degrees of look direction

		CitizenAnimationHelper animHelper = new CitizenAnimationHelper( Entity );

		animHelper.WithWishVelocity( controller.WishVelocity );
		animHelper.WithVelocity( controller.Velocity );
		animHelper.WithLookAt( Entity.AimRay.Position + inputRotation.Forward * 100f, 1f, 1f, 0f );
		animHelper.AimAngle = inputRotation;
		animHelper.FootShuffle = shuffle;
		animHelper.DuckLevel = MathX.Lerp( animHelper.DuckLevel, controller.HasTag( "ducked" ) ? 1 : 0, Time.Delta * 10f );

		animHelper.VoiceLevel = (Game.IsClient && Entity.Client.IsValid())
			? Entity.Client.Voice.LastHeard < 0.5f
			? Entity.Client.Voice.CurrentLevel : 0f : 0f;

		animHelper.IsGrounded = Entity.GroundEntity != null;
		animHelper.IsSitting = controller.HasTag( "sitting" );
		animHelper.IsClimbing = controller.HasTag( "climbing" );
		animHelper.IsSwimming = Entity.GetWaterLevel() >= 0.6f;
		animHelper.IsWeaponLowered = false;

		if ( controller.HasEvent( "jump" ) ) animHelper.TriggerJump();

		animHelper.HoldType = CitizenAnimationHelper.HoldTypes.None;
		animHelper.AimBodyWeight = 0.5f;
	}
}
