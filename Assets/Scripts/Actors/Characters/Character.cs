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
    #region IMOVABLE PROPERTIES

    public float MovementSpeed => _movementSpeed;
    [SerializeField] private float _movementSpeed = 1f;

    #endregion

    public NavMeshAgent Agent => _agent;
    [SerializeField] private NavMeshAgent _agent;

    public float RotationSpeed => _rotateSpeed;
    [SerializeField] private float _rotateSpeed = 0.2f;

    [SerializeField] private List<Gun> _availableWeapons;
    [SerializeField] private Gun _currentWeapon;
    [SerializeField] private AudioClip _leftStep;

    private bool        _isLeftStep = false;
    private float       _accMouseX = 0;          // reference for mouse look smoothing
    private AudioSource _audioSource;
    private static int  _currentCoins;

    private float mouseSnappiness = 20f;              // default was 10f; larger values of this cause less filtering, more responsiveness

    #region UNITY EVENTS

    public new void Start()
    {
        _currentCoins = 0;
        _audioSource  = GetComponent<AudioSource>();

        EquipWeapon(Weapons.Pistol);

        // Subscribe events
        EventManager.instance.OnNewKill += OnNewKill;
        EventManager.instance.OnSpend   += OnSpend;

        base.Start();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) EquipWeapon(Weapons.Pistol);
        if (Input.GetKeyDown(KeyCode.Alpha2)) EquipWeapon(Weapons.AssaultRifle);

        if (Input.GetKey(KeyCode.W)) Move(Vector3.forward);
        if (Input.GetKey(KeyCode.S)) Move(-Vector3.forward);
        if (Input.GetKey(KeyCode.A)) Move(-Vector3.right);
        if (Input.GetKey(KeyCode.D)) Move(Vector3.right);

        ProcessLook();

        if (Input.GetButton("Fire1")) _currentWeapon.Shoot();
        if (Input.GetKeyDown(KeyCode.R))  _currentWeapon.Reload();
    }

    #endregion

    #region LIFE CONTROLLER OVERRIDES

    public override void Die() 
    {
        EventManager.instance.ActionLevelVictory(false);
        Destroy(this.gameObject);
    }

    #endregion

    void ProcessLook()
    {
        float inputLookX = Input.GetAxis( "Mouse X" );
        _accMouseX = Mathf.Lerp( _accMouseX, inputLookX, mouseSnappiness * Time.deltaTime );
 
        float mouseX = _accMouseX * _rotateSpeed * 100f * Time.deltaTime;
       
        // Rotate player Y
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

    public void OnNewKill() {
        MainManager.instance.NewKill();
        if (MainManager.instance.TotalKills() % 5 == 0) {
            _currentCoins++;
            EventManager.instance.ActionCoinsChange(_currentCoins);
        }
    }

    public void OnSpend(int amount) 
    {
        if (_currentCoins >= amount) {
            _currentCoins -= amount;
            EventManager.instance.ActionCoinsChange(_currentCoins);
        }
    }

    #region IMOVABLE METHODS


    public void Move(Vector3 direction)
    {
        StartCoroutine(MoveSfxCoroutine());
        if (_agent.CalculatePath(transform.position + direction * MovementSpeed * Time.deltaTime, new UnityEngine.AI.NavMeshPath()))
        {
            transform.Translate(direction * MovementSpeed * Time.deltaTime);
        }
    }

    #endregion

    #region COROUTINES

    IEnumerator MoveSfxCoroutine()
    {
        if (!_audioSource.isPlaying)
        {
            if (_isLeftStep) {
                _audioSource.Play();
                _isLeftStep = false;
            } else {
                _audioSource.PlayOneShot(_leftStep);
                _isLeftStep = true;
            }
            yield return new WaitForSeconds(0.7f);
        }
    }

    #endregion
}
