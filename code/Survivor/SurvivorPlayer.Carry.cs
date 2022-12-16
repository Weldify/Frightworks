namespace Frightworks;

public partial class SurvivorPlayer
{
	[Net]
	public Carriable Carrying { get; set; }

	public void SimulateCarriable()
	{
		if ( !Carrying.IsValid() ) return;

		var animHelper = new CitizenAnimationHelper( this );
		Carrying.SimulateAnimator( animHelper );

		if ( !Game.IsServer ) return;

		if ( Input.Pressed( InputButton.Drop ) || LifeState != LifeState.Alive || !IsValid )
		{
			StopCarrying();
			return;
		}
	}

	public bool CanCarry()
	{
		return Carrying == null;
	}

	public void StartCarrying( Carriable carriable )
	{
		Carrying = carriable;
		carriable.CarryStart( this );
	}

	public void StopCarrying()
	{
		if ( !Game.IsServer ) return;
		if ( !Carrying.IsValid() ) return;

		Carrying.CarryStop();
		Carrying = null;
	}

	public void TryCarry( Carriable carriable )
	{
		if ( CanCarry() )
		{
			StartCarrying( carriable );
		}
	}
}
