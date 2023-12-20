using Godot;

public partial class Enemy : CharacterBody2D
{
	private float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	
	private HealthComponent _healthComponent;

	public override void _Ready()
	{
		_healthComponent = GetNode<HealthComponent>("HealthComponent");
		_healthComponent.HealthReachedZero += OnHealthReachedZero;
		_healthComponent.Health = 100;
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += _gravity * (float)delta;
		
		Velocity = velocity;
		MoveAndSlide();
	}

	public void Damage(int dmg)
	{
		_healthComponent.Damage(dmg);
	}

	private void OnHealthReachedZero()
	{
		QueueFree();
	}
}
