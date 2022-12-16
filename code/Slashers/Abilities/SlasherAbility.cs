namespace Frightworks;

public partial class SlasherAbility : Entity
{
	protected SlasherPlayer Slasher => Owner as SlasherPlayer;

	// If we can use, use and return true, otherwise return false.
	public bool Try()
	{
		if ( CanUse() )
		{
			SimulateUse();
			return true;
		}

		return false;
	}

	public virtual bool CanUse() => false;
	public virtual void SimulateUse() { }
}
