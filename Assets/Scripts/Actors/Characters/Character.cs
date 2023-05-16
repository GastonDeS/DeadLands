using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum Weapons
{
    Pistol = 0, MachineGun = 1, ShotGun = 2
}

public class Character : LifeController, IMovable
{
    [SerializeField] private List<Gun> _availableWeapons;
    [SerializeField] private Gun _currentWeapon;

    [SerializeField] private GameObject _bulletPrefab;

    public NavMeshAgent Agent => _agent;
    [SerializeField] private NavMeshAgent _agent;

    public float MovementSpeed => _movementSpeed;
    [SerializeField] private float _movementSpeed = 1f;

    public float RotationSpeed => _rotateSpeed;
    [SerializeField] private float _rotateSpeed = 0.2f;

    private float accMouseX = 0;                     // reference for mouse look smoothing
    public float mouseSnappiness = 20f;              // default was 10f; larger values of this cause less filtering, more responsiveness


    // Start is called before the first frame update
    public new void Start()
    {
        EquipWeapon(Weapons.Pistol);
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) EquipWeapon(Weapons.Pistol);
        if (Input.GetKeyDown(KeyCode.Alpha2)) EquipWeapon(Weapons.MachineGun);
        if (Input.GetKeyDown(KeyCode.Alpha3)) EquipWeapon(Weapons.ShotGun);

        if (Input.GetKey(KeyCode.W)) Move(Vector3.forward);
        if (Input.GetKey(KeyCode.S)) Move(-Vector3.forward);
        if (Input.GetKey(KeyCode.A)) Move(-Vector3.right);
        if (Input.GetKey(KeyCode.D)) Move(Vector3.right);

        ProcessLook();

        if (Input.GetKey(KeyCode.Return)) _currentWeapon.Shoot();
        if (Input.GetKeyDown(KeyCode.R)) _currentWeapon.Reload();

        // TODO: take damage dynamically
        // if (Input.GetKeyDown(KeyCode.Z)) GetComponent<LifeController>().TakeDamage(10);
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
        foreach (Gun gun in _availableWeapons)
        {
            gun.gameObject.SetActive(false);
        }
        _currentWeapon = _availableWeapons[(int) weapon];
        _currentWeapon.gameObject.SetActive(true);

        EventManager.instance.ActionWeaponChange((int) weapon);
    }

    public void Move(Vector3 direction)
    {
        if (_agent.CalculatePath(transform.position + direction * MovementSpeed * Time.deltaTime, new UnityEngine.AI.NavMeshPath()))
        {
            transform.Translate(direction * MovementSpeed * Time.deltaTime);
        }
    }
}
