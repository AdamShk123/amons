using Godot;
using Vector2 = Godot.Vector2;

public partial class Camera : Camera2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnPlayerPositionChanged(Vector2 pos)
	{
		Position = pos;
	}
}
