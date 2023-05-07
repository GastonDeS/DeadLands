using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGun
{
    //properties

    GameObject BulletPrefab { get; }
    int Damage { get; }
    int MagZise { get; }
    int CurrectBulletCount { get; }
    float ShotCooldown { get; }

    // Actions

    void Attack();

    void Reload();
}
