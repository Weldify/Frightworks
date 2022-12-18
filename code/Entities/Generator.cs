using Sandbox;

namespace Frightworks;

public partial class Generator : ModelEntity
{
	public CarBattery Battery;
	public GasCan GasCan;

	public float FuelLevel = 0f;
	public bool IsPowered = false;

	public override void Spawn()
	{
		SetModel( "models/generator/generator.vmdl" );
		SetupPhysicsFromModel( PhysicsMotionType.Static );

		Tags.Add( "solid" );

		EnableTouch = true;
	}

	void TryConnectBattery( Entity ent )
	{
		if ( ent is not CarBattery battery ) return;
		if ( Battery != null ) return;

		// Maybe its getting carried
		if ( battery.Owner is not null ) return;

		battery.SetParent( this, true );
		battery.Owner = this;

		battery.EnableAllCollisions = false;

		Battery = battery;

		TryPower();
	}

	void TryConnectGasCan( Entity ent )
	{
		if ( ent is not GasCan can || can.FuelRemaining == 0f ) return;
		if ( GasCan != null ) return;
		if ( FuelLevel == 1f ) return;

		// Maybe its getting carried
		if ( can.Owner is not null ) return;

		can.SetParent( this, true );
		can.Owner = this;

		can.EnableAllCollisions = false;
		can.EnableTraceAndQueries = true;

		GasCan = can;
	}

	void DetachGasCan()
	{
		if ( !GasCan.IsValid() ) return;
		GasCan.SetParent( null );
		GasCan.Owner = null;

		GasCan.EnableAllCollisions = true;
		GasCan.Velocity = Vector3.Up * 300f;

		GasCan.TryRemove();

		GasCan = null;

		TryPower();
	}

	// Try to siphon some fuel from the connected gas can
	// Returns whether we can keep fuelling
	public bool TryTickFuel()
	{
		if ( !GasCan.IsValid() ) return false;
		if ( GasCan.FuelRemaining == 0f || FuelLevel == 1f )
		{
			DetachGasCan();
			return false;
		}

		GasCan.PourSoundEnabled = true;

		// Make sure we don't siphon more than the generator can hold
		var toSiphon = MathF.Min( FuelLevel + Time.Delta * GameSettings.GasCanPourSpeed, 1f ) - FuelLevel;

		var siphoned = GasCan.Siphon( toSiphon );
		FuelLevel += siphoned;

		return true;
	}

	void TryPower()
	{
		if ( IsPowered ) return;
		if ( !Battery.IsValid() ) return;

		PlaySound( "generator.try_start" );

		if ( FuelLevel < 1f ) return;
		IsPowered = true;

		PlaySound( "generator.running" );
	}

	public override void StartTouch( Entity other )
	{
		Game.AssertServer();

		TryConnectBattery( other );
		TryConnectGasCan( other );
	}
}
