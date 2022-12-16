namespace Frightworks;

public partial class Ragdoll : ModelEntity
{
	public Ragdoll() { }

	public override void Spawn()
	{
		base.Spawn();

		Tags.Add( "debris" );
	}

	public void Init( ModelEntity from, Vector3 punchForce )
	{
		Game.AssertServer();

		Transform = from.Transform;

		UsePhysicsCollision = true;
		PhysicsEnabled = true;

		CopyFrom( from );
		this.CopyBonesFrom( from );
		this.SetRagdollVelocityFrom( from );

		PhysicsGroup.AddVelocity( punchForce );
	}
}
