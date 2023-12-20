using Godot;

public partial class Level : Node2D
{
	private Player _playerNode;

	private Camera _cameraNode;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_playerNode = GetNode<Player>("Player");
		_cameraNode = GetNode<Camera>("Camera");
		_playerNode.PositionChanged += _cameraNode.OnPlayerPositionChanged;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
