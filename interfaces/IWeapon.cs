using System.Collections.Generic;

namespace AMatterofNationalSecurity.interfaces;

public interface IWeapon
{
    public void Attack(IList<int> masks);
}