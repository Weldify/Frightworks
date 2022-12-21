namespace Frightworks;

[SlasherInfo( Title = "Terrance", SlasherType = SlasherType.Terrance )]
public partial class TerrancePlayer : SlasherPlayer
{
	public new static string Title => "Terrance";
	public new static SlasherType SlasherType => SlasherType.Terrance;

	public override void Spawn()
	{
		base.Spawn();

		SetModel( "models/citizen/citizen.vmdl" );
		RenderColor = Color.Red;

		AddAbility<SlasherBasicAttack>();
	}

	public override void Respawn()
	{
		base.Respawn();

		AnimationController = new HumanoidAnimator();
		CameraController = new FirstPersonCamera();

		var moveSpeed = GameSettings.SurvivorSprintSpeed * 1.15f;
		MoveController = new WalkController
		{
			DefaultSpeed = moveSpeed,
			WalkSpeed = moveSpeed,
			SprintSpeed = moveSpeed,
		};
	}
}
