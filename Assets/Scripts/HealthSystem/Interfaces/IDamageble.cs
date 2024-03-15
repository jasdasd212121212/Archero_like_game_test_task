using System;

public interface IDamageble
{
    event Action<int> Damaged;

    void TakeDamage(int damage);
}