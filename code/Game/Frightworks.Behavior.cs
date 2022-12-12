using Sandbox;

namespace Frightworks;

public partial class FrightworksGame
{
	[Net]
	GameBehavior _behavior { get; set; }

	public GameBehavior Behavior
	{
		get => _behavior;
		set
		{
			_behavior = value;
			_behavior.GameManager = this;
			_behavior.Activate();
		}
	}
}
