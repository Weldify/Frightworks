using System.Collections.Generic;

namespace Frightworks;

public class MatchTransferInfo
{
	public Dictionary<long, ReadyAs> PlayerRoles { get; set; } = new();
}
