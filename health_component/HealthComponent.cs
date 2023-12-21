using Godot;

public partial class HealthComponent : Node2D
{
	[Export]
	public float Health { get; set; }
	
	[Signal]
	public delegate void HealthReachedZeroEventHandler();

	public void Damage(float dmg)
	{
		Health -= dmg;
		if (Health <= 0)
		{
			Health = 0;
			EmitSignal(SignalName.HealthReachedZero);
		} 
	}
}
