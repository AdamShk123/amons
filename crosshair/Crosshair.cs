using Godot;

public partial class Crosshair : Sprite2D
{
	public override void _Process(double delta)
	{
		Position = GetGlobalMousePosition();
	}
}
