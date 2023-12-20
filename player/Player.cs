using Godot;
using Vector2 = Godot.Vector2;
	
public partial class Player : CharacterBody2D
{ 
	private const float MaxY = 300.0f;
	private const float MaxX = 200.0f;
	private const float Accel = 60.0f;
	
	private float Gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	
	[Signal]
	public delegate void PositionChangedEventHandler(Vector2 pos);

	private PackedScene BulletScene;

	public override void _Ready()
	{
		BulletScene = GD.Load<PackedScene>("res://bullet/bullet.tscn");
	}

	public override void _PhysicsProcess(double delta)
	{
		if (Input.IsActionJustPressed("shoot"))
		{
			Bullet bulletNode = BulletScene.Instantiate<Bullet>();
			bulletNode.Dir = GetLocalMousePosition().Normalized();
			bulletNode.Position = Position;
			bulletNode.ShooterId = GetInstanceId();
			GetParent().AddChild(bulletNode);
		}
		
		Vector2 velocity = Velocity;

		if (!IsOnFloor())
		{
			float change = (float)(Gravity * delta);
			velocity.Y = Mathf.MoveToward(Velocity.Y, MaxY, change);
		}

		if (Input.IsActionJustPressed("jump") && IsOnFloor())
			velocity.Y = -MaxY;
		
		float dir = Input.GetAxis("left", "right");
		if(dir != 0)
		{
			velocity.X = Mathf.MoveToward(Velocity.X, MaxX * dir, Accel);
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Accel);
		}

		Vector2 oldPos = Position;
		Velocity = velocity;
		MoveAndSlide();
		if (!oldPos.IsEqualApprox(Position))
		{
			EmitSignal(SignalName.PositionChanged, Position);
		}
	}
}
