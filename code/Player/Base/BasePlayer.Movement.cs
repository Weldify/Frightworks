using Sandbox;

namespace Frightworks;

public partial class BasePlayer
{
	public MoveController MoveController
	{
		get => Components.Get<MoveController>();
		set
		{
			if ( value == null )
			{
				Components.RemoveAny<MoveController>();
				return;
			}

			Components.Add( value );
		}
	}

	void DoMovement()
	{
		MoveController?.Simulate( Client, this );
	}
}
