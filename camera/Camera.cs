using Godot;
using Vector2 = Godot.Vector2;

public partial class Camera : Camera2D
{
	public override void _Ready()
	{
	}
	
	public override void _Process(double delta)
	{
	}

	public void OnPlayerPositionChanged(Vector2 pos)
	{
		Position = pos;
	}
}
