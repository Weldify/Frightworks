namespace Frightworks;

public partial class SurvivorPlayer
{
	enum TerrorProximity
	{
		Far,
		Close,
		Inaudible,
	}

	TerrorProximity terrorProximity = TerrorProximity.Inaudible;

	Sound terrorSound;

	float? GetDistanceFromSlasher()
	{
		var slashers = All.OfType<SlasherPlayer>()
			.Where( s => s.IsValid() && s.LifeState == LifeState.Alive );
		if ( !slashers.Any() ) return null;

		return Position.Distance( slashers.First().Position );
	}

	void ChangeTerrorSound( float dist )
	{
		var oldProximity = terrorProximity;

		if ( dist < GameSettings.DefaultTerrorRadius * 0.3f )
			terrorProximity = TerrorProximity.Close;
		else if ( dist < GameSettings.DefaultTerrorRadius )
			terrorProximity = TerrorProximity.Far;
		else if ( dist > GameSettings.DefaultTerrorRadius )
			terrorProximity = TerrorProximity.Inaudible;

		if ( oldProximity == terrorProximity ) return;

		terrorSound.Stop();

		if ( terrorProximity == TerrorProximity.Inaudible ) return;

		var str = terrorProximity switch
		{
			TerrorProximity.Close => "close",
			TerrorProximity.Far or _ => "far",
		};
		terrorSound = Sound.FromScreen( $"slasher.proximity.{str}" );
	}

	void TickTerrorSound()
	{
		// If there is no slasher, this is null
		var distOption = GetDistanceFromSlasher();
		if ( distOption is not float dist ) return;

		ChangeTerrorSound( dist );

		var loudness = MathF.Min( 1f, (1f - dist / GameSettings.DefaultTerrorRadius) * 2f );
		terrorSound.SetVolume( loudness );
	}
}
