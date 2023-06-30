using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitableObject : MonoBehaviour, IHitable
{
    public void Apply(Vector3 direction)
    {
        GetComponent<Rigidbody>().AddForce(direction * 1000);
    }
}
