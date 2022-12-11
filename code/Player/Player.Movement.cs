using Sandbox;

namespace Frightworks;

public partial class Player
{
	public MoveController MoveController
	{
		get => Components.Get<MoveController>();
		set => Components.Add( value );
	}

	void DoMovement()
	{
		MoveController?.Simulate( Client, this );
	}
}
