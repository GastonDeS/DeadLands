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

    private bool hit = false;

    void Start()
    {
        _lifetimeController = this.gameObject.GetComponent<LifeTimeController>();
        _lifetimeController.SetLifeTime(_lifetime);
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
        if (hit) return;
        hit = true;
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        damageable?.TakeDamage(10); // TODO add damage value
        Debug.Log("Bullet collided with " + collision.gameObject.name);
        Destroy(this.gameObject);
    }
}
