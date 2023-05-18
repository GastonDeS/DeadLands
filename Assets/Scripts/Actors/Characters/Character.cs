using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum Weapons
{
    Pistol = 0, AssaultRifle = 1
}

[RequireComponent(typeof(AudioSource))]
public class Character : LifeController, IMovable
{
    [SerializeField] private List<Gun> _availableWeapons;
    [SerializeField] private Gun _currentWeapon;

    public NavMeshAgent Agent => _agent;
    [SerializeField] private NavMeshAgent _agent;

    public float MovementSpeed => _movementSpeed;
    [SerializeField] private float _movementSpeed = 1f;

    public float RotationSpeed => _rotateSpeed;
    [SerializeField] private float _rotateSpeed = 0.2f;

    private float accMouseX = 0;                     // reference for mouse look smoothing
    public float mouseSnappiness = 20f;              // default was 10f; larger values of this cause less filtering, more responsiveness

    private AudioSource _audioSource;
    [SerializeField] private AudioClip leftStep;
    private bool isLeftStep = false;

    private int _currentCoins;
    private int _totalKills;
    private int _currentLevel;

    // Start is called before the first frame update
    public new void Start()
    {
        _totalKills   = 0;
        _currentLevel = 1;
        _currentCoins = 200;
        _audioSource  = GetComponent<AudioSource>();
        
        EquipWeapon(Weapons.Pistol);
        EventManager.instance.OnNewKill += OnNewKill;
        EventManager.instance.OnSpend += OnSpend;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) EquipWeapon(Weapons.Pistol);
        if (Input.GetKeyDown(KeyCode.Alpha2)) EquipWeapon(Weapons.AssaultRifle);

        if (Input.GetKey(KeyCode.W)) Move(Vector3.forward);
        if (Input.GetKey(KeyCode.S)) Move(-Vector3.forward);
        if (Input.GetKey(KeyCode.A)) Move(-Vector3.right);
        if (Input.GetKey(KeyCode.D)) Move(Vector3.right);

        ProcessLook();

        if (Input.GetKey(KeyCode.Return)) _currentWeapon.Shoot();
        if (Input.GetKeyDown(KeyCode.R)) _currentWeapon.Reload();

        // TODO: take damage dynamically
        if (Input.GetKeyDown(KeyCode.Z)) GetComponent<LifeController>().Die();
    }

    public override void Die() 
    {
        EventManager.instance.ActionDefeat(_currentLevel, _totalKills);
        EventManager.instance.ActionLevelVictory(false);
        Destroy(this.gameObject);
    }

    void ProcessLook()
    {
        float inputLookX = Input.GetAxis( "Mouse X" );
        accMouseX = Mathf.Lerp( accMouseX, inputLookX, mouseSnappiness * Time.deltaTime );
 
        float mouseX = accMouseX * _rotateSpeed * 100f * Time.deltaTime;
       
        // rotate player Y
        transform.Rotate( Vector3.up * mouseX );
    }

    private void EquipWeapon(Weapons weapon)
    {
        if ( _currentWeapon != null && _currentWeapon.IsReloading ) return;
        foreach (Gun gun in _availableWeapons)
        {
            gun.gameObject.SetActive(false);
        }
        _currentWeapon = _availableWeapons[(int) weapon];
        _currentWeapon.gameObject.SetActive(true);
        EventManager.instance?.ActionWeaponChange((int) weapon);
        _currentWeapon.SetAmmo();
    }

    public void Move(Vector3 direction)
    {
        StartCoroutine(MoveSfxCoroutine());
        if (_agent.CalculatePath(transform.position + direction * MovementSpeed * Time.deltaTime, new UnityEngine.AI.NavMeshPath()))
        {
            transform.Translate(direction * MovementSpeed * Time.deltaTime);
        }
    }

    IEnumerator MoveSfxCoroutine()
    {
        if (!_audioSource.isPlaying)
        {
            if (isLeftStep) {
                _audioSource.Play();
                isLeftStep = false;
            } else {
                _audioSource.PlayOneShot(leftStep);
                isLeftStep = true;
            }
            yield return new WaitForSeconds(0.7f);
        }
    }

    public void OnNewKill() {
        _totalKills++;
        if (_totalKills % 5 == 0) {
            _currentCoins++;
        }
    }

    public void OnSpend(int amount) 
    {
        if (_currentCoins >= amount) {
            _currentCoins -= amount;
            EventManager.instance.ActionCoinsChange(_currentCoins);
        }
    }
}
