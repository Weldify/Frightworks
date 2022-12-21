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

	PointLightEntity slasherLight;
	void CreateSlasherLight()
	{
		Log.Info( "bals" );
		slasherLight = new()
		{
			Brightness = 0.35f,
			Color = new Color( 255f, 83f, 83f ),
			Range = 1024f,
			Owner = this,
		};
	}

	public override void Simulate( IClient cl )
	{
		base.Simulate( cl );

		if ( Game.IsClient && !slasherLight.IsValid() )
			CreateSlasherLight();

		SimulateAbilities();
	}

	public override void FrameSimulate( IClient cl )
	{
		base.FrameSimulate( cl );

		if ( slasherLight.IsValid() )
		{
			slasherLight.Position = Position + new Vector3( 0f, 0f, 48f );
		}
	}

	public override float FootstepVolume()
	{
		return 2f;
	}
}
