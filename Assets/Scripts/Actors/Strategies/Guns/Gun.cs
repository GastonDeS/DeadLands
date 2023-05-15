using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour, IGun
{
    public GameObject BulletPrefab => _bulletPrefab;
    [SerializeField] private GameObject _bulletPrefab;

    public int Damage => _damage;
    [SerializeField] private int _damage = 10;

    public int MagZise => _magSize;
    [SerializeField] private int _magSize = 20;

    public int CurrectBulletCount => _currentBulletCount;
    [SerializeField] private int _currentBulletCount;

    public float ShotCooldown => _shootCooldown;
    [SerializeField] private float _shootCooldown = .5f;
    private float _currentShootCooldown = 0;

    private AudioSource _audioSource;

    public void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public virtual void Attack()
    {
        if (HasBullets() && _currentShootCooldown <= 0)
        {
            _currentShootCooldown = _shootCooldown;
            _currentBulletCount--;
            Instantiate(_bulletPrefab, transform.position + transform.forward * 1, transform.rotation);
            _audioSource?.Play();
        }
    }

    protected bool HasBullets() => CurrectBulletCount > 0;

    public virtual void Reload() => _currentBulletCount = _magSize;

    private void Update()
    {
        if (_currentBulletCount >= 0)
        {
            _currentShootCooldown -= Time.deltaTime;
        }
    }

}
