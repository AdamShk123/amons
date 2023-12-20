using Godot;

public partial class Level : Node2D
{
	private Player PlayerNode;

	private Camera CameraNode;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PlayerNode = GetNode<Player>("Player");
		CameraNode = GetNode<Camera>("Camera");
		PlayerNode.PositionChanged += CameraNode.OnPlayerPositionChanged;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
