namespace Frightworks;

public enum SlasherType
{
	Terrance,
	None
}

class SlasherInfoAttribute : Attribute
{
	public string Title { get; set; }
	public SlasherType SlasherType { get; set; }
}

public partial class SlasherPlayer : BasePlayer
{
	public static string Title => "Slasher";
	public static SlasherType SlasherType => SlasherType.None;
}
