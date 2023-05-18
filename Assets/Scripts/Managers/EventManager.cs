using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public void ActionCharacterLifeChange(float currentLife, float maxLife) {
        OnCharacterLifeChange?.Invoke(currentLife, maxLife);
    }

    public event Action<int> OnWeaponChange;
<<<<<<< HEAD
    public void ActionWeaponChange(int index) 
    {
        OnWeaponChange?.Invoke(index);
=======
    public void ActionWeaponChange(int index) {
       if (OnWeaponChange != null) OnWeaponChange(index);
>>>>>>> 24e75ff (feat: add coins logic)
    }

    public event Action<int, int> OnWeaponAmmoChange;
    public void ActionWeaponAmmoChange(int currentAmmo, int maxAmmo) {
        OnWeaponAmmoChange?.Invoke(currentAmmo, maxAmmo);
    }

    #endregion

    #region COINS

    public event Action<int> OnCoinsChange;
    public void ActionCoinsChange(int currentCoins) 
    {
        if (OnCoinsChange != null) OnCoinsChange(currentCoins);
    }

    #endregion

    #region STATS

    public event Action<int, int> OnDefeat;
    public void ActionDefeat(int level, int totalKills) 
    {
        if (OnDefeat != null) OnDefeat(level, totalKills);
    }

    public event Action OnNewKill;
    public void ActionNewKill() 
    {
        if (OnNewKill != null) OnNewKill();
    }

    public event Action OnRecoverLife;
    public void ActionRecoverLife() 
    {
        if (OnRecoverLife != null) OnRecoverLife();
    }

    public event Action<int> OnSpend;
    public void ActionSpend(int amount) 
    {
        if (OnSpend != null) OnSpend(amount);
    }

    #endregion
}
