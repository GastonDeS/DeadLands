﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGun
{
    // Properties

    int Damage { get; }
    int MagZise { get; }
    int CurrentBulletCount { get; }
    float ShotCooldown { get; }

    // Actions
    void Shoot();
    void Reload();
}
