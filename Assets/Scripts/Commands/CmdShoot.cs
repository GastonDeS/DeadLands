using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdShoot : ICommand
{
    private IGun _gun;

    public CmdShoot(IGun gun) 
    {
        _gun = gun;
    }

    public void SetGun(IGun newGun)
    {
        _gun = newGun;
    }

    public void Execute() => _gun.Shoot();
}
