using System.Collections;
using UnityEngine;

public class Coins : MonoBehaviour, ICoins
{
  //private static int _lifePrice = 100;

  public int CurrentCoins => _currentCoins;
  [SerializeField] private int _currentCoins = 200;

  public void Start()
  {
    _currentCoins = 200;
    EventManager.instance.ActionCoinsChange(_currentCoins);
  }

  public void BuyLife() 
  {
    // if (_currentCoins >= _lifePrice) {
    //   _currentCoins -= _lifePrice;
    //   Debug.Log(EventManager.instance == null);
    //   GetComponent<LifeController>().RecoverFullLife();
    //   EventManager.instance.ActionCoinsChange(_currentCoins);
    // }
  }
}