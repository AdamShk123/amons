using System.Collections.Generic;
using Godot;
using AMatterofNationalSecurity.interfaces;

public partial class Pistol : Node2D, IWeapon
{
	private PackedScene _bulletScene;

	private AudioStreamPlayer2D _audioStreamPlayer;

	[Export]
	private float _speed;
	
	[Export]
	private float _damage;
	
	public override void _Ready()
	{
		_bulletScene = GD.Load<PackedScene>("res://bullet/bullet.tscn");

		_audioStreamPlayer = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
	}

	public void Attack(IList<int> masks)
	{
		var bulletNode = _bulletScene.Instantiate<Bullet>();
		
		var direction = (GetGlobalMousePosition() - GetViewport().GetCamera2D().GetScreenCenterPosition()).Normalized();
		
		bulletNode.Speed = _speed;
		bulletNode.Dmg = _damage;
		bulletNode.Dir = direction;
		bulletNode.Position = GetParent<Node2D>().Position;
		
		foreach (var mask in masks)
		{
			bulletNode.SetCollisionMaskValue(mask, true);	
		}
		
		_audioStreamPlayer.Play();
		
		GetParent().GetParent().AddChild(bulletNode);
	}
}
