namespace Frightworks;

public partial class BasePlayer
{
	public AnimationController AnimationController
	{
		get => Components.Get<AnimationController>();
		set => Components.Add( value );
	}

	void DoAnimation()
	{
		AnimationController?.Simulate();
	}
}
