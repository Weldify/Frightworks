namespace Frightworks;

public partial class SlasherBasicAttack : SlasherAbility
{
	public override bool CanUse()
	{
		return Input.Pressed( InputButton.PrimaryAttack );
	}

	public override void SimulateUse()
	{
		Log.Info("Amongla fard");
	}
}
