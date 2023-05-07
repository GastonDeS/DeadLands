using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRotable
{
    float RotationSpeed { get; }

    void Rotation(Vector3 dirrection);
}
