using Sandbox;
using System.Collections.Generic;

namespace Frightworks;

public partial class MoveController : EntityComponent<BasePlayer>, ISingletonComponent
{
	internal HashSet<string> Events;
	internal HashSet<string> Tags;

	public Vector3 Position { get; set; }
	public Rotation Rotation { get; set; }
	public Vector3 Velocity { get; set; }
	public Vector3 BaseVelocity { get; set; }
	public Entity GroundEntity { get; set; }
	public Vector3 GroundNormal { get; set; }

	public Vector3 WishVelocity { get; set; }

	public float EyeHeightScale { get; set; }

	public void UpdateFromEntity( BasePlayer plr )
	{
		Position = plr.Position;
		Rotation = plr.Rotation;
		Velocity = plr.Velocity;
		BaseVelocity = plr.BaseVelocity;
		GroundEntity = plr.GroundEntity;
		WishVelocity = plr.Velocity;
		EyeHeightScale = plr.EyeHeightScale;
	}

	public void UpdateFromController( MoveController controller )
	{
		Position = controller.Position;
		Rotation = controller.Rotation;
		Velocity = controller.Velocity;
		GroundEntity = controller.GroundEntity;
		BaseVelocity = controller.BaseVelocity;
		WishVelocity = controller.WishVelocity;
		GroundNormal = controller.GroundNormal;
		EyeHeightScale = controller.EyeHeightScale;

		Events = controller.Events;
		Tags = controller.Tags;
	}

	public void Finalize( BasePlayer target )
	{
		target.Position = Position;
		target.Velocity = Velocity;
		target.Rotation = Rotation;
		target.GroundEntity = GroundEntity;
		target.BaseVelocity = BaseVelocity;
		target.EyeHeightScale = EyeHeightScale;
	}

	/// <summary>
	/// This is what your logic should be going in
	/// </summary>
	public virtual void Simulate()
	{
		// Nothing
	}

	/// <summary>
	/// This is called every frame on the client only
	/// </summary>
	public virtual void FrameSimulate()
	{
		
	}

	/// <summary>
	/// Call OnEvent for each event
	/// </summary>
	public virtual void RunEvents()
	{
		if ( Events == null ) return;

		foreach ( var e in Events )
		{
			OnEvent( e );
		}
	}

	/// <summary>
	/// An event has been triggered - maybe handle it
	/// </summary>
	public virtual void OnEvent( string name )
	{
	}

	/// <summary>
	/// Returns true if we have this event
	/// </summary>
	public bool HasEvent( string eventName )
	{
		if ( Events == null ) return false;
		return Events.Contains( eventName );
	}

	/// <summary>
	/// </summary>
	public bool HasTag( string tagName )
	{
		if ( Tags == null ) return false;
		return Tags.Contains( tagName );
	}


	/// <summary>
	/// Allows the controller to pass events to other systems
	/// while staying abstracted.
	/// For example, it could pass a "jump" event, which could then
	/// be picked up by the playeranimator to trigger a jump animation,
	/// and picked up by the player to play a jump sound.
	/// </summary>
	public void AddEvent( string eventName )
	{
		// TODO - shall we allow passing data with the event?

		if ( Events == null ) Events = new HashSet<string>();
	
		if ( Events.Contains( eventName ) )
			return;

		Events.Add( eventName );
	}


	/// <summary>
	/// </summary>
	public void SetTag( string tagName )
	{
		// TODO - shall we allow passing data with the event?

		Tags ??= new HashSet<string>();

		if ( Tags.Contains( tagName ) )
			return;

		Tags.Add( tagName );
	}

	public void Simulate( IClient client, BasePlayer plr )
	{
		Events?.Clear();
		Tags?.Clear();

		UpdateFromEntity( plr );

		Simulate();

		Finalize( plr );
	}

	public void FrameSimulate( IClient client, BasePlayer plr )
	{
		UpdateFromEntity( plr );

		FrameSimulate();

		Finalize( plr );
	}
}
