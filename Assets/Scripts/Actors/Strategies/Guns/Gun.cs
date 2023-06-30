using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour, IGun
{
    public GunStats GunStats => _gunStats;
    [SerializeField] private GunStats _gunStats;

    [SerializeField] private bool AddBulletSpread = true;
    [SerializeField] private Vector3 BulletSpreadVariance = new Vector3(0.02f, 0.02f, 0.02f);
    [SerializeField] private ParticleSystem ShootingSystem;
    [SerializeField] private Transform BulletSpawnPoint;
    [SerializeField] private ParticleSystem ImpactParticleSystem;
    [SerializeField] private TrailRenderer BulletTrail;
    [SerializeField] private LayerMask Mask;
    [SerializeField] private float BulletSpeed = 100;

    public int Damage         => _gunStats.Damage;
    public int MagSize        => _gunStats.MagSize;
    public float ShotCooldown => _gunStats.ShotCooldown;

    public int CurrentBulletCount => _currentBulletCount;
    [SerializeField] private int _currentBulletCount;

    [SerializeField] private Camera _camera;
    private float LastShootTime;

    public bool IsReloading => _isReloading;
    private bool _isReloading = false;

    private AudioSource _audioSource;
    [SerializeField] AudioClip _reloadSound;
    private Animator _animator;

    public void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _currentBulletCount = MagSize;
    }

    public void Reload()
    {
        if (_isReloading || _currentBulletCount == MagSize) return;
        StartCoroutine(ReloadCoroutine());
    }

    public void SetAmmo() 
    {
        EventManager.instance?.ActionWeaponAmmoChange(_currentBulletCount, MagSize);
    }

    IEnumerator ReloadCoroutine()
    {
        _isReloading = true;
        _audioSource.PlayOneShot(_reloadSound);
        yield return new WaitForSeconds(1f);
        _currentBulletCount = MagSize;
        _isReloading = false;
        EventManager.instance?.ActionWeaponAmmoChange(_currentBulletCount, MagSize);
    }

    public void Shoot()
    {
        if (_isReloading) return;
        if (LastShootTime + ShotCooldown < Time.time && _currentBulletCount > 0)
        {
            // Use an object pool instead for these! To keep this tutorial focused, we'll skip implementing one.
            // For more details you can see: https://youtu.be/fsDE_mO4RZM or if using Unity 2021+: https://youtu.be/zyzqA_CPz2E
            _currentBulletCount--;
            EventManager.instance?.ActionWeaponAmmoChange(_currentBulletCount, MagSize);

            ShootingSystem.Play();
            Vector3 direction = GetDirection();
            _audioSource?.Play();
            if (Physics.Raycast(_camera.transform.position, direction, out RaycastHit hit, float.MaxValue))
            {
                TrailRenderer trail = Instantiate(BulletTrail, BulletSpawnPoint.position, Quaternion.identity);

                IDamageable damageable = hit.collider.GetComponentInParent<IDamageable>();
                bool isDamageable = damageable != null;
                if (isDamageable && gameObject.GetComponentInParent<IDamageable>() != damageable)
                {
                    damageable.TakeDamage(Damage);
                } else if (hit.collider.GetComponentInParent<IHitable>() != null) {
                    hit.collider.GetComponentInParent<IHitable>().Apply(direction);
                }

                StartCoroutine(SpawnTrail(trail, hit.point, hit.normal, true, isDamageable));

                LastShootTime = Time.time;
            }
            // this has been updated to fix a commonly reported problem that you cannot fire if you would not hit anything
            else
            {
                TrailRenderer trail = Instantiate(BulletTrail, BulletSpawnPoint.position, Quaternion.identity);

                StartCoroutine(SpawnTrail(trail, BulletSpawnPoint.position + GetDirection() * 100, Vector3.zero, false, false));

                LastShootTime = Time.time;
            }
        }
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = BulletSpawnPoint.forward;

        if (AddBulletSpread)
        {
            direction += new Vector3(
                Random.Range(-BulletSpreadVariance.x, BulletSpreadVariance.x),
                Random.Range(-BulletSpreadVariance.y, BulletSpreadVariance.y),
                Random.Range(-BulletSpreadVariance.z, BulletSpreadVariance.z)
            );

            direction.Normalize();
        }

        return direction;
    }

    private IEnumerator SpawnTrail(TrailRenderer Trail, Vector3 HitPoint, Vector3 HitNormal, bool MadeImpact, bool isDamageable)
    {
        // This has been updated from the video implementation to fix a commonly raised issue about the bullet trails
        // moving slowly when hitting something close, and not
        Vector3 startPosition = Trail.transform.position;
        float distance = Vector3.Distance(Trail.transform.position, HitPoint);
        float remainingDistance = distance;

        while (remainingDistance > 0)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, HitPoint, 1 - (remainingDistance / distance));

            remainingDistance -= BulletSpeed * Time.deltaTime;

            yield return null;
        }
        Trail.transform.position = HitPoint;
        if (MadeImpact && !isDamageable)
        {
            Instantiate(ImpactParticleSystem, HitPoint, Quaternion.LookRotation(HitNormal));
        }

        Destroy(Trail.gameObject, Trail.time);
    }
}