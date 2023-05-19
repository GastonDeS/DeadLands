using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour, IDamageable
{
    #region IDAMAGEABLE PROPERTIES

    public int MaxLife => _maxLife;
    [SerializeField] private int _maxLife = 100;

    public int CurrentLife => _currentLife;
    [SerializeField] private int _currentLife = 0;

    #endregion

    [SerializeField] private bool _isMainCharacter = false;

    #region UNITY EVENTS

    public virtual void Start()
    {
        _currentLife = _maxLife;
        if (_isMainCharacter) ActionUpdateUILife();

        // Subscribe event
        EventManager.instance.OnRecoverLife += OnRecoverLife;
    }

    #endregion

    #region IDAMAGEABLE METHODS

    public void TakeDamage(int damage)
    {
        _currentLife -= damage;

        if (_isMainCharacter) ActionUpdateUILife();

        if (!IsAlive()) Die();
    }

    public bool IsAlive() => _currentLife > 0;

    public void RecoverLife(int amount)
    {
        if (_currentLife + amount >= _maxLife) {
            _currentLife = _maxLife;
        } else {
            _currentLife += amount;
        }
    }

    #endregion

    #region ABSTRACT METHODS

    public virtual void Die()
    {
        // Must be implemented by child classes
    }

    #endregion
    
    public void UpdateMaxLife(int newMaxLife) 
    {
        _maxLife = newMaxLife;
        _currentLife = _maxLife;
        if (_isMainCharacter) ActionUpdateUILife();
    }

    public void OnRecoverLife() 
    {
        _currentLife = _maxLife;
        ActionUpdateUILife();
    }

    private void ActionUpdateUILife() => EventManager.instance?.ActionCharacterLifeChange((float) CurrentLife, (float) MaxLife);

}
