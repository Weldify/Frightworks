using Sandbox;

namespace Frightworks;

public partial class SurvivorPlayer
{
	[Net, Predicted, Local]
	bool FlashlightEnabled { get; set; }

	SpotLightEntity worldLight;
	SpotLightEntity viewLight;

	SpotLightEntity CreateLight()
	{
		return new SpotLightEntity
		{
			Enabled = false,
			DynamicShadows = true,
			Range = 512,
			Falloff = 1.0f,
			LinearAttenuation = 0.0f,
			QuadraticAttenuation = 1.0f,
			Brightness = 2,
			Color = Color.White,
			InnerConeAngle = 20,
			OuterConeAngle = 40,
			FogStrength = 1.0f,
			Owner = this,
			LightCookie = Texture.Load( "materials/effects/lightcookie.vtex" ),
		};
	}

	void InitFlashlight()
	{
		worldLight = CreateLight();
		worldLight.EnableHideInFirstPerson = true;
		worldLight.SetParent( this, "eyes", new Transform( Vector3.Forward * 10f ) );
	}

	void InitFlashlightClient()
	{
		viewLight = CreateLight();
		viewLight.EnableViewmodelRendering = true;
	}

	void UpdateClientFlashlightTransform()
	{
		Game.AssertClient();

		var rot = Camera.Main.Rotation;
		viewLight.Transform = new Transform( Camera.Main.Position + (rot.Forward * 5f), rot );
	}

	void FrameSimulateFlashlight()
	{
		UpdateClientFlashlightTransform();
	}

	bool firstFlashlightSim = true;
	void SimulateFlashlight()
	{
		if ( !Prediction.FirstTime ) return;

		var dead = LifeState != LifeState.Alive;
		if ( !dead && !Input.Pressed( InputButton.Flashlight ) ) return;

		// If we are dead, turn off the flashlight
		var newFlashlightEnabled = dead ? false : !FlashlightEnabled;

		if ( newFlashlightEnabled == FlashlightEnabled ) return;
		FlashlightEnabled = newFlashlightEnabled;

		if ( FlashlightEnabled )
			PlaySound( "flashlight.switch_on" );
		else
			PlaySound( "flashlight.switch_off" );

		if ( worldLight.IsValid() )
			worldLight.Enabled = FlashlightEnabled;

		if ( Game.IsClient )
		{
			if ( firstFlashlightSim )
			{
				firstFlashlightSim = false;
				InitFlashlightClient();
			}

			viewLight.Enabled = FlashlightEnabled;
		}
	}
}
