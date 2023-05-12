using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour, IDamageable
{
    public int MaxLife => _maxLife;
    [SerializeField] private int _maxLife = 100;

    public int CurrentLife => _currentLife;
    [SerializeField] private int _currentLife = 0;

    public void RecoverLife(int amount) => _currentLife -= amount;

    public bool IsAlive() => _currentLife > 0;

    public void Die() => Destroy(this.gameObject);

    public void Start()
    {
        _currentLife = _maxLife;
    }

    public void TakeDamage(int damage)
    {
        _currentLife -= damage;
        Debug.Log("LifeController: " + this.gameObject.name + " took " + damage + " damage");
        if (!IsAlive()) Die();
    }

}
