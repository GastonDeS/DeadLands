using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICoins
{
    // Properties
    int CurrentCoins { get; }

    // Actions
    void BuyLife();
}