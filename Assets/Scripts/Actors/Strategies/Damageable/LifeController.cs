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

    public void RecoverLife(int amount) 
    {
        _currentLife += amount;
        ActionUpdateUILife();
    }

    public bool IsAlive() => _currentLife > 0;

    public virtual void Die()
    {
        if (_isMainCharacter) EventManager.instance.ActionLevelVictory(false);
        // TODO solve fix
        Destroy(this.gameObject);
    }

    public void Start()
    {
        _currentLife = _maxLife;
        ActionUpdateUILife();
    }

    public void TakeDamage(int damage)
    {
        _currentLife -= damage;

        if (_isMainCharacter) ActionUpdateUILife();

        if (!IsAlive()) Die();
    }

    public void UpdateMaxLife(int newMaxLife) 
    {
        _maxLife = newMaxLife;
        _currentLife = _maxLife;
        if (_isMainCharacter) ActionUpdateUILife();
    }

    private void ActionUpdateUILife() => EventManager.instance.ActionCharacterLifeChange((float) CurrentLife, (float) MaxLife);

}
