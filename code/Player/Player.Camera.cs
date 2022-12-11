namespace Frightworks;

public partial class Player
{
	public CameraController CameraController
	{
		get => Components.Get<CameraController>();
		set => Components.Add( value );
	}

	void UpdateCamera()
	{
		CameraController?.Update();
	}
}
