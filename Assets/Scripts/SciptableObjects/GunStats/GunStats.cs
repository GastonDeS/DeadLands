using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunStats", menuName = "Stats/Guns/BasicGun", order = 0)]
public class GunStats : ScriptableObject
{
    [SerializeField] private GunStatValues _stats;

    public int Damage         => _stats.Damage;
    public int MagSize        => _stats.MagSize;
    public float ShotCooldown => _stats.ShotCooldown;
    public int NumberOfPellets => _stats.NumberOfPellets;
    public float PelletSpread => _stats.PelletSpread;

}

[System.Serializable]
public struct GunStatValues
{
    public int Damage;
    public int MagSize;
    public float ShotCooldown;
    public int NumberOfPellets;
    public float PelletSpread; 
    
}
