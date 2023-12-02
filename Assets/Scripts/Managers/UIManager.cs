using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Utilities;

public class UIManager : MonoBehaviour
{
    #region PROPERTIES

    [SerializeField] private Image           _lifeBar;
    [SerializeField] private TextMeshProUGUI _lifeValue;

    [SerializeField] private TextMeshProUGUI _coinValue;

    [SerializeField] private List<Sprite>    _weaponSprites;
    [SerializeField] private Image           _weapon;
    [SerializeField] private TextMeshProUGUI _ammo;

    [SerializeField] private TextMeshProUGUI _level;

    [SerializeField] private GameObject _lifeHolder;
    [SerializeField] private GameObject _rifleHolder;
    [SerializeField] private GameObject _shotGunHolder;

    private float xPosChange;

    #endregion

    #region UNITY EVENTS

    private void Start() 
    {
        EventManager.instance.OnCharacterLifeChange += OnCharacterLifeChange;
        EventManager.instance.OnWeaponChange        += OnWeaponChange;
        EventManager.instance.OnWeaponAmmoChange    += OnWeaponAmmoChange;
        EventManager.instance.OnCoinsChange         += OnCoinsChange;
        EventManager.instance.OnLevelChange         += OnLevelChange;
        EventManager.instance.OnMarketChange        += OnMarketChange;

        xPosChange = (_rifleHolder.transform.position.x - _lifeHolder.transform.position.x)/2;
    }

    #endregion

    #region SUBSCRIBERS

    private void OnCharacterLifeChange(float currentLife, float maxLife)
    {
        _lifeBar.fillAmount = currentLife / maxLife;
        _lifeValue.text     = $"{(currentLife / maxLife) * 100} %";
    }

    private void OnWeaponChange(int index) => _weapon.sprite = _weaponSprites[index];
    
    private void OnWeaponAmmoChange(int currentAmmo, int maxAmmo) => _ammo.text = $"{currentAmmo}/{maxAmmo}";

    private void OnCoinsChange(int currentCoins) => _coinValue.text    = $"{currentCoins}";

    private void OnLevelChange(int level) => _level.text = $"{level}";

    private void OnMarketChange(Weapons weapon) {
        if (weapon == Weapons.AssaultRifle) { 
            AcquireRifle();
        } else {
            AcquireShotGun();
        }
    }

    private void AcquireRifle() {
        _rifleHolder.SetActive(false);
        if (_shotGunHolder.activeSelf) {
            _shotGunHolder.transform.position -= new Vector3(xPosChange, 0f, 0f);
        }
        _lifeHolder.transform.position += new Vector3(xPosChange, 0f, 0f);
    }

    private void AcquireShotGun() {
        _shotGunHolder.SetActive(false);
        if (_rifleHolder.activeSelf) {
            _rifleHolder.transform.position += new Vector3(xPosChange, 0f, 0f);
        }
            _lifeHolder.transform.position += new Vector3(xPosChange, 0f, 0f);

    }

    #endregion
}
