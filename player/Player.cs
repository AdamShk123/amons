using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AMatterofNationalSecurity.interfaces;
using Godot;
using Vector2 = Godot.Vector2;


	
public partial class Player : CharacterBody2D, IDamage
{ 
	private const float MaxY = 300.0f;
	private const float MaxX = 200.0f;
	private const float Accel = 60.0f;
	private const float CoyoteTime = 0.2f;
	
	private float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	
	[Signal]
	public delegate void PositionChangedEventHandler(Vector2 pos);

	private HealthComponent _healthComponent;
	
	private AnimatedSprite2D _animatedSprite2D;
	
	private readonly IList<HitboxComponent> _hitboxComponents = new List<HitboxComponent>();

	private Timer _timerNode;

	private IWeapon _weapon;

	[Export] 
	public int Ammo;
	
	public override void _Ready()
	{
		_healthComponent = GetNode<HealthComponent>("HealthComponent");

		_hitboxComponents.Add(GetNode<HitboxComponent>("Head"));
		_hitboxComponents.Add(GetNode<HitboxComponent>("Torso"));
		_hitboxComponents.Add(GetNode<HitboxComponent>("Legs"));

		
		_animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_animatedSprite2D.Play();

		_timerNode = GetNode<Timer>("Timer");

		_weapon = GetNode<IWeapon>("Pistol");
	}

	public override void _PhysicsProcess(double delta)
	{
		if (Input.IsActionJustPressed("shoot") && Ammo > 0)
		{
			_weapon.Attack(new List<int>{(int)AMatterofNationalSecurity.CollisionLayer.Enemy});
			Ammo -= 1;
		}
		
		var velocity = Velocity;

		if (!IsOnFloor())
		{
			float change = (float)(_gravity * delta);
			velocity.Y = Mathf.MoveToward(Velocity.Y, MaxY, change);
		}
		else
		{
			_timerNode.Start(CoyoteTime);
		}

		if (_timerNode.TimeLeft > 0 && Input.IsActionJustPressed("jump"))
		{
			velocity.Y = -MaxY;
			_timerNode.Stop();
		}

		if (Input.IsActionJustReleased("jump") && velocity.Y < 0)
		{
			velocity.Y *= 0.5f;
		}

		float mul = 0.0f;
		if (Input.IsActionPressed("left"))
		{
			mul -= 1.0f;
		}
		
		if (Input.IsActionPressed("right"))
		{
			mul += 1.0f;
		}
			
		velocity.X = !mul.Equals(0.0f) ? Mathf.MoveToward(Velocity.X, MaxX * mul, Accel) : Mathf.MoveToward(Velocity.X, 0, Accel);

		var oldPos = Position;
		Velocity = velocity;
		MoveAndSlide();
		if (!oldPos.IsEqualApprox(Position))
		{
			EmitSignal(SignalName.PositionChanged, Position);
		}
	}

	public void Damage(float dmg, Area2D area)
	{
		throw new NotImplementedException();
	}

	public void OnHealthReachedZero()
	{
		throw new NotImplementedException();
	}
}
