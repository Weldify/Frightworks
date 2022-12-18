namespace Frightworks;

public interface IContinuousUse
{
	void OnUseStop();
}

public partial class Carriable : ModelEntity, IUse
{
	public SurvivorPlayer Survivor => Owner as SurvivorPlayer;

	public virtual Model WorldModel => Model.Load( "models/dev/error.vmdl" );
	public virtual Model ViewModel => Model.Load( "models/dev/error.vmdl" );
	protected ViewModel ViewModelEntity;

	public override void Spawn()
	{
		base.Spawn();

		Tags.Add( "solid" );

		Model = WorldModel;
		SetupPhysicsFromModel( PhysicsMotionType.Dynamic );

		PhysicsEnabled = true;
		UsePhysicsCollision = true;
		EnableHideInFirstPerson = true;
		EnableShadowInFirstPerson = true;
	}

	[ClientRpc]
	protected virtual void CreateViewModel() { }

	[ClientRpc]
	void RemoveViewModel()
	{
		ViewModelEntity?.Delete();
	}

	public virtual bool CanCarry()
	{
		return Owner == null;
	}

	public virtual void CarryStart( SurvivorPlayer carrier )
	{
		Game.AssertServer();

		SetParent( carrier, true );
		Owner = carrier;
		EnableAllCollisions = false;

		CreateViewModel( To.Single( Owner ) );
	}

	public virtual void CarryStop()
	{
		Game.AssertServer();

		var owner = Owner;

		SetParent( null );
		Owner = null;
		EnableAllCollisions = true;

		if ( !owner.IsValid() ) return;

		Position = owner.AimRay.Position + owner.AimRay.Forward * 30f;
		Rotation = Rotation.LookAt( owner.AimRay.Forward );
		Velocity = owner.Velocity + owner.AimRay.Forward * 100f;
		RemoveViewModel( To.Single( owner ) );
	}

	public virtual bool IsUsable( Entity ent )
	{
		return ent.IsValid()
			&& ent is SurvivorPlayer survivor
			&& survivor.LifeState == LifeState.Alive;
	}

	public virtual bool OnUse( Entity ent )
	{
		if ( ent is not SurvivorPlayer survivor ) return false;

		if ( CanCarry() )
		{
			survivor.TryCarry( this );
		}

		return false;
	}

	public virtual void SimulateAnimator( CitizenAnimationHelper anim ) { }
}
