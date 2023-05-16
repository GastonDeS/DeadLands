using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    private void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;
    }

    #region UI_ELEMENTS

    public event Action<float, float> OnCharacterLifeChange;
    public void ActionCharacterLifeChange(float currentLife, float maxLife) => OnCharacterLifeChange(currentLife, maxLife);

    public event Action<int> OnWeaponChange;
    public void ActionWeaponChange(int index) => OnWeaponChange(index);

    public event Action<int, int> OnWeaponAmmoChange;
    public void ActionWeaponAmmoChange(int currentAmmo, int maxAmmo) => OnWeaponAmmoChange(currentAmmo, maxAmmo);

    #endregion
}