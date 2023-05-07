using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Gun
{
    public class MachineGun : Gun
    {
        [SerializeField] private float _bulletPerShell = 5;

        public override void Attack()
        {
            for (int i = 0; i < _bulletPerShell; i++)
            {
                Instantiate(
                    BulletPrefab,
                    Random.insideUnitSphere * 2 + transform.forward * 2 * 0.6f + transform.position,
                    transform.rotation);
            }
        }
    }
}
