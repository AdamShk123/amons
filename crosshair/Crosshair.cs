using Godot;

public partial class Crosshair : Sprite2D
{
	private Vector2 _center;
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		Position = _center + (GetViewport().GetMousePosition() - GetViewportRect().Size / 2.0f) / 3.0f;
	}

	public void OnCameraPositionChanged(Vector2 pos)
	{
		_center = pos;
	}
}
