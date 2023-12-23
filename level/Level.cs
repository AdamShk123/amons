using Godot;

public partial class Level : Node2D
{
	public override void _Ready()
	{
	}
	
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("close"))
		{
			GetTree().Quit();
		}
	}
}
