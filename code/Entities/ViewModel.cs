namespace Frightworks;

public class ViewModel : AnimatedEntity
{
	static ViewModel currentViewModel;

	public Vector3 PositionOffset { get; set; } = Vector3.Zero;

	public override void Spawn()
	{
		base.Spawn();

		Log.Info( "spawner" );

		if ( currentViewModel.IsValid() )
		{
			currentViewModel.Delete();
			Log.Info( "deleted" );
		}

		currentViewModel = this;

		EnableViewmodelRendering = true;
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
