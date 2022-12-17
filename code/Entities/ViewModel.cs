namespace Frightworks;

public class ViewModel : AnimatedEntity
{
	static ViewModel currentViewModel;

	public Vector3 PositionOffset { get; set; } = Vector3.Zero;

	public override void Spawn()
	{
		base.Spawn();

		if ( currentViewModel.IsValid() )
		{
			currentViewModel.Delete();
		}

		EnableViewmodelRendering = true;

		Owner = Game.LocalPawn;
		SetParent( Game.LocalPawn );
		currentViewModel = this;
	}

	[Event.Client.Frame]
	public void OnFrame()
	{
		Camera.Main.SetViewModelCamera( Camera.Main.FieldOfView );
	}

	[Event.Client.PostCamera]
	public void OnPostCamera()
	{
		var camTransform = new Transform( Camera.Position, Camera.Rotation );

		Position = camTransform.PointToWorld( PositionOffset );
		Rotation = camTransform.Rotation;
	}
}
