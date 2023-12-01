using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Utilities;

[RequireComponent(typeof(AudioSource))]
public class Character : LifeController
{
    private MovementController _movementController;

    public float RotationSpeed => _rotateSpeed;
    [SerializeField] private float _rotateSpeed = 0.2f;

    [SerializeField] private List<Gun> _availableWeapons;
    [SerializeField] private Gun _currentWeapon;

    private float _accMouseX = 0;          // reference for mouse look smoothing
    private static int _currentCoins;

    private float mouseSnappiness = 20f;              // default was 10f; larger values of this cause less filtering, more responsiveness

    private List<Weapons> _acquiredWeapons = new List<Weapons>();
    private int _currentWeaponIdx = 0;

    #region COMMANDS

    private CmdMovement _cmdMoveForward;
    private CmdMovement _cmdMoveBackward;
    private CmdMovement _cmdMoveLeft;
    private CmdMovement _cmdMoveRight;

    private CmdJump _cmdJump;

    private CmdShoot _cmdShoot;
    private CmdReload _cmdReload;

    #endregion

    #region UNITY EVENTS

    public new void Start()
    {
        _movementController = GetComponent<MovementController>();
        _currentCoins = 0;

        EquipWeapon(Weapons.ShotGun);
        _acquiredWeapons.Add(Weapons.ShotGun);

        // Subscribe events
        EventManager.instance.OnNewKill       += OnNewKill;
        EventManager.instance.OnSpend         += OnSpend;
        EventManager.instance.OnAcquireWeapon += OnAcquireWeapon;

        _cmdMoveForward = new CmdMovement(_movementController, Vector3.forward);
        _cmdMoveBackward = new CmdMovement(_movementController, -Vector3.forward);
        _cmdMoveLeft = new CmdMovement(_movementController, -Vector3.right);
        _cmdMoveRight = new CmdMovement(_movementController, Vector3.right);
        _cmdJump = new CmdJump(_movementController);

        _cmdShoot = new CmdShoot(_currentWeapon);
        _cmdReload = new CmdReload(_currentWeapon);

        base.Start();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeWeapon();
        }
        // if (Input.GetKeyDown(KeyCode.Alpha2))
        // {
        //     EquipWeapon(Weapons.AssaultRifle);
        //     _cmdShoot.SetGun(_currentWeapon);
        //     _cmdReload.SetGun(_currentWeapon);
        // }

        if (Input.GetKey(KeyCode.W)) _cmdMoveForward.Execute();
        if (Input.GetKey(KeyCode.S)) _cmdMoveBackward.Execute();
        if (Input.GetKey(KeyCode.A)) _cmdMoveLeft.Execute();
        if (Input.GetKey(KeyCode.D)) _cmdMoveRight.Execute();
        if (Input.GetKeyDown(KeyCode.Space)) _cmdJump.Execute();

        ProcessLook();

        if (Input.GetButton("Fire1")) _cmdShoot.Execute();
        if (Input.GetKeyDown(KeyCode.R)) _cmdReload.Execute();
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
        float inputLookX = Input.GetAxis("Mouse X");
        _accMouseX = Mathf.Lerp(_accMouseX, inputLookX, mouseSnappiness * Time.deltaTime);

        float mouseX = _accMouseX * _rotateSpeed * 100f * Time.deltaTime;

        // Rotate player Y
        transform.Rotate(Vector3.up * mouseX);
    }

    private void ChangeWeapon() {
        int size = _acquiredWeapons.Count;
        if (_currentWeaponIdx == size - 1) {
            _currentWeaponIdx = 0;
        } else {
            _currentWeaponIdx++;
        }
        Weapons newWeapon = _acquiredWeapons[_currentWeaponIdx];
        EquipWeapon(newWeapon);
        _cmdShoot.SetGun(_currentWeapon);
        _cmdReload.SetGun(_currentWeapon);
    }

    private void OnAcquireWeapon(Weapons weapon)
    {
        _acquiredWeapons.Add(weapon);
    }

    private void EquipWeapon(Weapons weapon)
    {
        if (_currentWeapon != null && _currentWeapon.IsReloading) return;
        foreach (Gun gun in _availableWeapons)
        {
            gun.gameObject.SetActive(false);
        }
        _currentWeapon = _availableWeapons[(int)weapon];
        _currentWeapon.gameObject.SetActive(true);
        EventManager.instance?.ActionWeaponChange((int)weapon);
        _currentWeapon.SetAmmo();
    }

    public void OnNewKill()
    {
        MainManager.instance.NewKill();
        if (MainManager.instance.TotalKills() % 5 == 0)
        {
            _currentCoins++;
            EventManager.instance.ActionCoinsChange(_currentCoins);
        }
    }

    public bool OnSpend(int amount)
    {
        if (_currentCoins >= amount)
        {
            _currentCoins -= amount;
            EventManager.instance.ActionCoinsChange(_currentCoins);
            return true;
        }
        return false;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider != null && collision.gameObject.layer == 7)
        {
            _movementController.Land();
            return;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.collider != null && collision.gameObject.layer == 7) return;
    }
}
