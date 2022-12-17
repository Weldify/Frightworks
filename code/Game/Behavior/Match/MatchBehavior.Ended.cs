namespace Frightworks;

public enum EndReason
{
	SurvivorsDead,
	SurvivorsEscaped
}

public partial class MatchBehavior
{
	[Net]
	public EndReason EndReason { get; set; }

	TimeSince timeSinceEnded;

	bool CanBecomeEnded()
	{
		return AreAllSurvivorsDead();
	}

	void BecomeEnded()
	{
		State = MatchState.Ended;

		if ( AreAllSurvivorsDead() )
			EndReason = EndReason.SurvivorsDead;

		timeSinceEnded = 0f;
	}

	void TickEnded()
	{
		if ( timeSinceEnded < 10f ) return;

		Game.ChangeLevel( GameSettings.LobbyMapIdent );
	}

	// Killer win condition
	bool AreAllSurvivorsDead()
	{
		var alive = Entity.All.OfType<SurvivorPlayer>()
			.Where( p => p.IsValid() && p.LifeState == LifeState.Alive );

		return !alive.Any();
	}
}
