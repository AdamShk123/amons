using AMatterofNationalSecurity.interfaces;
using Godot;
using Vector2 = Godot.Vector2;

public partial class Bullet : Area2D
{
	[Export]
	private float _speed = 300.0f;

	public Vector2 Dir { get; set; }
	
	[Export]
	public float Dmg { get; set; }

	private Timer _timerNode;

	public override void _Ready()
	{
		_timerNode = GetNode<Timer>("Timer");
		_timerNode.Start();
	}
	
	public override void _Process(double delta)
	{
		var position = Position;
		position += (float)delta * _speed * Dir;
		Position = position;
	}
	
	private void OnBodyEntered(Node2D body)
	{
		QueueFree();
	}
	
	private void OnTimerTimeout()
	{
		QueueFree();
	}

	private void OnAreaEntered(Area2D area)
	{
		if (GetCollisionMaskValue(4))
		{
			((IDamage)area.GetParent()).Damage(Dmg, area);
			SetCollisionMaskValue(4, false);
			QueueFree();	
		} 
	}
}