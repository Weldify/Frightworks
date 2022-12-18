namespace Frightworks;

public partial class BasePlayer
{
	/// <summary>
	/// Entity the player is currently using via their interaction key.
	/// </summary>
	public Entity Using { get; protected set; }

	/// <summary>
	/// This should be called somewhere in your player's tick to allow them to use entities
	/// </summary>
	protected virtual void DoUse()
	{
		// This is serverside only
		if ( !Game.IsServer ) return;

		// Turn prediction off
		using ( Prediction.Off() )
		{
			if ( Input.Pressed( InputButton.Use ) )
			{
				Using = FindUsable();

				if ( Using == null )
				{
					UseFail();
					return;
				}
			}

			if ( !Input.Down( InputButton.Use ) )
			{
				StopUsing();
				return;
			}

			if ( !Using.IsValid() )
				return;

			// If we move too far away or something we should probably ClearUse()?

			//
			// If use returns true then we can keep using it
			//
			if ( Using is IUse use && use.OnUse( this ) )
				return;

			StopUsing();
		}
	}

	protected virtual void UseFail()
	{
		PlaySound( "player_use_fail" );
	}

	protected virtual void StopUsing()
	{
		if (Using.IsValid() && Using is IContinuousUse use)
		{
			use.OnUseStop();
		}

		Using = null;
	}

	protected bool IsValidUseEntity( Entity e )
	{
		if ( e == null ) return false;
		if ( e is not IUse use ) return false;
		if ( !use.IsUsable( this ) ) return false;

		return true;
	}

	protected virtual Entity FindUsable()
	{
		// First try a direct 0 width line
		var tr = Trace.Ray( AimRay.Position, AimRay.Position + AimRay.Forward * 85 )
			.Ignore( this )
			.Run();

		// See if any of the parent entities are usable if we ain't.
		var ent = tr.Entity;
		while ( ent.IsValid() && !IsValidUseEntity( ent ) )
		{
			ent = ent.Parent;
		}

		// Nothing found, try a wider search
		if ( !IsValidUseEntity( ent ) )
		{
			tr = Trace.Ray( AimRay.Position, AimRay.Position + AimRay.Forward * 85 )
			.Radius( 2 )
			.Ignore( this )
			.Run();

			// See if any of the parent entities are usable if we ain't.
			ent = tr.Entity;
			while ( ent.IsValid() && !IsValidUseEntity( ent ) )
			{
				ent = ent.Parent;
			}
		}

		// Still no good? Bail.
		if ( !IsValidUseEntity( ent ) ) return null;

		return ent;
	}
}
