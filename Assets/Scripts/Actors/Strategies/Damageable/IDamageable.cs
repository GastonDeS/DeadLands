using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    // Properties
    public int MaxLife { get; }
    public int CurrentLife { get; }

    // Actions
    void TakeDamage(int damage);
    void RecoverLife(int amount);
    bool IsAlive();
}
