using Godot;

public partial class HealthComponent : Node2D
{
	public int Health { get; set; }
	
	[Signal]
	public delegate void HealthReachedZeroEventHandler();
	
	public override void _Ready()
	{
		Health = 100;
	}

	public void Damage(int dmg)
	{
		Health -= dmg;
		if (Health <= 0)
		{
			Health = 0;
			EmitSignal(SignalName.HealthReachedZero);
		} 
	}
}
