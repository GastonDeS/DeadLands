using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour, IDamageable
{
    public int MaxLife => _maxLife;
    [SerializeField] private int _maxLife = 100;

    public int CurrentLife => _currentLife;
    [SerializeField] private int _currentLife = 0;

    [SerializeField] private bool _isMainCharacter = false;

    public void RecoverLife(int amount) => _currentLife += amount;

    public bool IsAlive() => _currentLife > 0;

    public virtual void Die()
    {
        Destroy(this.gameObject);
    }

    public void Start()
    {
        _currentLife = _maxLife;
    }

    public void TakeDamage(int damage)
    {
        _currentLife -= damage;

        if (_isMainCharacter) EventManager.instance.ActionCharacterLifeChange((float) CurrentLife, (float) MaxLife);

        if (!IsAlive()) Die();
    }

}
