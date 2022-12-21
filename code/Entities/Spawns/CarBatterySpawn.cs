using Editor;
using Sandbox;

namespace Frightworks;

[Library( "fw_car_battery" ), HammerEntity]
[Title( "Car Battery Spawn" ), Category( "Objective" )]
[EditorModel( "models/car_battery/car_battery.vmdl" )]
public partial class CarBatterySpawn : Entity 
{
	[Property( Title = "Generator Id" )]
	public int GeneratorId { get; set; } = 0;

	public override void Spawn()
	{
		Transmit = TransmitType.Never;
	}
}
