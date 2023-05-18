using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
     // Properties
    [SerializeField] private Image           _lifeBar;
    [SerializeField] private TextMeshProUGUI _lifeValue;

    [SerializeField] private TextMeshProUGUI _coinValue;

    [SerializeField] private List<Sprite>    _weaponSprites;
    [SerializeField] private Image           _weapon;
    [SerializeField] private TextMeshProUGUI _ammo;

    private void Start() 
    {
        EventManager.instance.OnCharacterLifeChange += OnCharacterLifeChange;
        EventManager.instance.OnWeaponChange        += OnWeaponChange;
        EventManager.instance.OnWeaponAmmoChange    += OnWeaponAmmoChange;
        EventManager.instance.OnCoinsChange         += OnCoinsChange;
    }

    private void OnCharacterLifeChange(float currentLife, float maxLife)
    {
        _lifeBar.fillAmount = currentLife / maxLife;
        _lifeValue.text     = $"{(currentLife / maxLife) * 100} %";
    }

    private void OnWeaponChange(int index) => _weapon.sprite = _weaponSprites[index];
    
    private void OnWeaponAmmoChange(int currentAmmo, int maxAmmo) => _ammo.text = $"{currentAmmo}/{maxAmmo}";

    private void OnCoinsChange(int currentCoins) => _coinValue.text    = $"{currentCoins}";
}
