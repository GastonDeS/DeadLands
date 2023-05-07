using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Gun
{
    [SerializeField] private float _bulletCountPerShot = 5;

    public override void Attack()
    {
        for (int i =0; i < _bulletCountPerShot; i++)
        {
            Instantiate(BulletPrefab, transform.position + transform.forward * (0.6f * (i + 2)), transform.rotation);
        }
    }
}
