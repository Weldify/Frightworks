using System.Collections.Generic;

namespace Frightworks;

public class MatchTransferInfo
{
	public Dictionary<long, ReadyAs> PlayerRoles { get; set; } = new();
	// How many of the players are bots
	public int BotCount { get; set; } = 0;
}
