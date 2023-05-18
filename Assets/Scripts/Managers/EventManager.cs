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
    public void ActionCharacterLifeChange(float currentLife, float maxLife) => OnCharacterLifeChange(currentLife, maxLife);

    public event Action<int> OnWeaponChange;
    public void ActionWeaponChange(int index) {
        OnWeaponChange?.Invoke(index);
    }

    public event Action<int, int> OnWeaponAmmoChange;
    public void ActionWeaponAmmoChange(int currentAmmo, int maxAmmo) => OnWeaponAmmoChange(currentAmmo, maxAmmo);

    #endregion

    #region COINS

    public event Action<int> OnCoinsChange;
    public void ActionCoinsChange(int currentCoins) => OnCoinsChange(currentCoins);

    #endregion
}
