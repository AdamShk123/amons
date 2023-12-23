using Godot;

namespace AMatterofNationalSecurity.interfaces;

public interface IDamage
{
    public void Damage(float dmg, Area2D area);

    public void OnHealthReachedZero();
}