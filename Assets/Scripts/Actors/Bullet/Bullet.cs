using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LifeTimeController))]
public class Bullet : MonoBehaviour, IBullet, IMovable
{
    private LifeTimeController _lifetimeController;

    public float MovementSpeed => _movableSpeed;
    [SerializeField] private float _movableSpeed = 15f;

    public float LifeTime => _lifetime;
    [SerializeField] private float _lifetime = 3f;

    void Start()
    {
        _lifetimeController = this.gameObject.GetComponent<LifeTimeController>();
        _lifetimeController.SetLifeTime(_lifetime);
        Debug.Log("lf " + _lifetimeController.IsLifeTimeOver());
    }

    void Update()
    {
        Travel();
        if (_lifetimeController.IsLifeTimeOver()) Destroy(this.gameObject);
    }

    public void Travel() => Move(Vector3.forward);

    public void Move(Vector3 direction) {
        transform.Translate(Vector3.forward* Time.deltaTime* MovementSpeed);
    }

    public void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        damageable?.TakeDamage(10);
    }
}
