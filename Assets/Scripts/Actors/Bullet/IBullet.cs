using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet
{
    float LifeTime { get; }

    void Travel();
    void OnCollisionEnter(Collision collision);
}
