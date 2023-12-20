using Godot;
using Vector2 = Godot.Vector2;

public partial class Bullet : Area2D
{
	private const float Speed = 600.0f;

	public Vector2 Dir { get; set; }

	private Timer _timerNode;

	public ulong ShooterId { get; set; }

	public override void _Ready()
	{
		_timerNode = GetNode<Timer>("Timer");
		_timerNode.Start();
	}
	
	public override void _Process(double delta)
	{
		Vector2 position = Position;
		position += (float)delta * Speed * Dir;
		Position = position;
	}
	
	private void OnBodyEntered(Node2D body)
	{
		if (body.GetInstanceId().Equals(ShooterId))
		{
			return;
		}

		if (body.IsInGroup("enemies") || body is Enemy)
		{
			((Enemy)body).Damage(50);
		}
		
		QueueFree();
	}
	
	private void OnTimerTimeout()
	{
		QueueFree();
	}
}