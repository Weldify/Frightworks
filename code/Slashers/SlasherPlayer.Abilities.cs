namespace Frightworks;

public partial class SlasherPlayer
{
	[Net, Predicted]
	IList<SlasherAbility> Abilities { get; set; }

	public T AddAbility<T>() where T : SlasherAbility, new()
	{
		T ability = new()
		{
			Owner = this
		};

		Abilities.Add( ability );

		return ability;
	}

	public void SimulateAbilities()
	{
		foreach ( var ability in Abilities )
		{
			ability.Try();
		}
	}
}
