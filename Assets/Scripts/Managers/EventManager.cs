using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    #region UNITY_EVENTS

    private void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;
        Debug.Log("EventManager instance assigned.");
    }

    #endregion

    #region GAME_MANAGER

    public event Action<bool> OnLevelVictory;
    public void ActionLevelVictory(bool _isVictory) => OnLevelVictory(_isVictory);

    #endregion

    #region UI_ELEMENTS

    public event Action<float, float> OnCharacterLifeChange;
    public void ActionCharacterLifeChange(float currentLife, float maxLife)
    {
        if (OnCharacterLifeChange != null) OnCharacterLifeChange(currentLife, maxLife);
    }

    public event Action<int> OnWeaponChange;
    public void ActionWeaponChange(int index)
    {
        if (OnWeaponChange != null) OnWeaponChange(index);
    }

    public event Action<int, int> OnWeaponAmmoChange;
    public void ActionWeaponAmmoChange(int currentAmmo, int maxAmmo)
    {
        if (OnWeaponAmmoChange != null) OnWeaponAmmoChange(currentAmmo, maxAmmo);
    }

    public event Action<int> OnLevelChange;
    public void ActionLevelChange(int level)
    {
        if (OnLevelChange != null) OnLevelChange(level);
    }

    public event Action<int> OnCoinsChange;
    public void ActionCoinsChange(int currentCoins)
    {
        if (OnCoinsChange != null) OnCoinsChange(currentCoins);
    }

    #endregion

    #region MARKET

    public event Action OnRecoverLife;
    public void ActionRecoverLife()
    {
        if (OnRecoverLife != null) OnRecoverLife();
    }

    public event Func<int, bool> OnSpend;
    public bool ActionSpend(int amount)
    {
        if (OnSpend != null) return OnSpend(amount);
        return false;
    }

    public event Action<Weapons> OnAcquireWeapon;
    public void ActionAcquireWeapon(Weapons weapon)
    {
        if (OnAcquireWeapon != null) OnAcquireWeapon(weapon);
    }

    #endregion

    #region ENEMY KILLS

    public event Action OnNewKill;
    public void ActionNewKill()
    {
        if (OnNewKill != null) OnNewKill();
    }

    public event Action OnBoostEnemies;
    public void ActionBoostEnemies()
    {
        if (OnBoostEnemies != null) OnBoostEnemies();
    }

    public event Action OnDistributeEnemies;
    public void ActionDistributeEnemies()
    {
        if (OnDistributeEnemies != null) OnDistributeEnemies();
    }

    #endregion
}
