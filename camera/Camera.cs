using Godot;
using Vector2 = Godot.Vector2;

public partial class Camera : Camera2D
{
	[Signal]
	public delegate void PositionChangedEventHandler(Vector2 pos);

	private Vector2 _oldPos;
		
	public override void _Ready()
	{
	}
	
	public override void _Process(double delta)
	{
		if (!_oldPos.Equals(GetScreenCenterPosition()))
		{
			EmitSignal(SignalName.PositionChanged, GetScreenCenterPosition());	
		}
		
		_oldPos = GetScreenCenterPosition();;
	}

	public void OnPlayerPositionChanged(Vector2 pos)
	{
		Position = pos;
	}
}
