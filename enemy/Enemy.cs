using System.Collections.Generic;
using AMatterofNationalSecurity.interfaces;
using Godot;

public partial class Enemy : CharacterBody2D, IDamage
{
	private float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	
	private HealthComponent _healthComponent;

	private readonly IList<HitboxComponent> _hitboxComponents = new List<HitboxComponent>();

	private AnimatedSprite2D _animatedSprite2D;

	private AnimatedSprite2D _deathAnimation;

	public override void _Ready()
	{
		_healthComponent = GetNode<HealthComponent>("HealthComponent");
		_healthComponent.HealthReachedZero += OnHealthReachedZero;
		_healthComponent.Health = 100.0f;
		
		_hitboxComponents.Add(GetNode<HitboxComponent>("Head"));
		_hitboxComponents.Add(GetNode<HitboxComponent>("Torso"));
		_hitboxComponents.Add(GetNode<HitboxComponent>("Legs"));

		_animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_animatedSprite2D.Play();

		_deathAnimation = GetNode<AnimatedSprite2D>("DeathAnimation");
		_deathAnimation.AnimationFinished += _on_death_animation_animation_finished;
	}

	public override void _PhysicsProcess(double delta)
	{
		var velocity = Velocity;
		
		if (!IsOnFloor())
			velocity.Y += _gravity * (float)delta;
		
		Velocity = velocity;
		MoveAndSlide();
	}

	public void Damage(float dmg, Area2D area)
	{
		foreach (var hitboxComponent in _hitboxComponents)
		{
			if (area.Equals(hitboxComponent))
			{
				float total = hitboxComponent.Multiplier * dmg;
				_healthComponent.Damage(total);
				return;
			}
		}
	}

	private void OnHealthReachedZero()
	{
		_animatedSprite2D.Visible = false;
		_deathAnimation.Visible = true;
		_deathAnimation.Play(); ;
	}

	private void _on_death_animation_animation_finished()
	{
		_deathAnimation.Visible = false;
		QueueFree();
	}
}
