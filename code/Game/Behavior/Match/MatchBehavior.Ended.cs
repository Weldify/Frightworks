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
		return AreAllSurvivorsDead()
			|| HaveAllSurvivorsEscaped();
	}

	void BecomeEnded()
	{
		State = MatchState.Ended;

		if ( AreAllSurvivorsDead() )
			EndReason = EndReason.SurvivorsDead;

		if ( HaveAllSurvivorsEscaped() )
			EndReason = EndReason.SurvivorsEscaped;

		timeSinceEnded = 0f;
	}

	void TickEndedState()
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

	// Survivor win condition
	bool HaveAllSurvivorsEscaped()
	{
		var alive = Entity.All.OfType<SurvivorPlayer>()
			.Where( p => p.IsValid() && p.LifeState == LifeState.Alive );

		var inHeli = alive.Where( p => p.IsInHelicopter );

		return alive.Any() && alive.Count() == inHeli.Count();
	}
}
