using System;
using AMatterofNationalSecurity.interfaces;
using Godot;
using Vector2 = Godot.Vector2;
	
public partial class Player : CharacterBody2D, IDamage
{ 
	private const float MaxY = 300.0f;
	private const float MaxX = 200.0f;
	private const float Accel = 60.0f;
	
	private float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	
	[Signal]
	public delegate void PositionChangedEventHandler(Vector2 pos);

	private PackedScene _bulletScene;

	private HealthComponent _healthComponent;

	private HitboxComponent _hitboxComponent;
	
	private AnimatedSprite2D _animatedSprite2D;

	public override void _Ready()
	{
		_bulletScene = GD.Load<PackedScene>("res://bullet/bullet.tscn");
		
		_healthComponent = GetNode<HealthComponent>("HealthComponent");
		_healthComponent.Health = 100.0f;

		_hitboxComponent = GetNode<HitboxComponent>("HitboxComponent");
		
		_animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_animatedSprite2D.Play();
	}

	public override void _PhysicsProcess(double delta)
	{
		if (Input.IsActionJustPressed("shoot"))
		{
			var bulletNode = _bulletScene.Instantiate<Bullet>();
			bulletNode.Dir = GetLocalMousePosition().Normalized();
			bulletNode.Dmg = 50.0f;
			bulletNode.Position = Position;
			bulletNode.SetCollisionMaskValue((int)AMatterofNationalSecurity.CollisionLayer.Enemy, true);
			GetParent().AddChild(bulletNode);
		}
		
		var velocity = Velocity;

		if (!IsOnFloor())
		{
			float change = (float)(_gravity * delta);
			velocity.Y = Mathf.MoveToward(Velocity.Y, MaxY, change);
		}

		if (Input.IsActionJustPressed("jump") && IsOnFloor())
			velocity.Y = -MaxY;
		
		var dir = Input.GetAxis("left", "right");
		
		velocity.X = dir != 0 ? Mathf.MoveToward(Velocity.X, MaxX * dir, Accel) : Mathf.MoveToward(Velocity.X, 0, Accel);

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
}
