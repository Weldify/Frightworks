using Sandbox;
using System;
using System.Linq;

namespace Frightworks;

public enum MatchState
{
	WaitingForPlayers,
	Ongoing,
	Ended,
}

public partial class MatchBehavior
{
	[Net]
	public MatchState State { get; set; } = MatchState.WaitingForPlayers;

	public void TickState()
	{
		// Weird ass state machine
		while ( true )
		{
			switch ( State )
			{
				case MatchState.WaitingForPlayers:
					if ( CanBecomeOngoing() )
					{
						BecomeOngoing();
						continue;
					}

					break;
				case MatchState.Ongoing:
					if ( CanBecomeEnded() )
					{
						BecomeEnded();
						continue;
					}

					TickOngoingState();

					break;
				case MatchState.Ended:
					TickEndedState();
					break;
			}

			break;
		}
	}
}
