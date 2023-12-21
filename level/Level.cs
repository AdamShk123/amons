using Godot;

public partial class Level : Node2D
{
	private Player _playerNode;

	private Camera _cameraNode;

	private Crosshair _crosshairNode;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_cameraNode = GetNode<Camera>("Camera");
		
		_playerNode = GetNode<Player>("Player");
		
		_playerNode.PositionChanged += _cameraNode.OnPlayerPositionChanged;

		_crosshairNode = GetNode<Crosshair>("Crosshair");
		_cameraNode.PositionChanged += _crosshairNode.OnCameraPositionChanged;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
