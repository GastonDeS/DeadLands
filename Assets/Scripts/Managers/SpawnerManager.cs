using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    private int _levelEnemiesAmount = 5;
    private float _levelEnemiesStatsBoostPercent = 1;
    private Spawner[] _spawners;

    #region UNITY EVENTS

    void Start()
    {
        EventManager.instance.OnBoostEnemies      += BoostEnemies;
        EventManager.instance.OnDistributeEnemies += DistributeZombies;

        _spawners = GetComponentsInChildren<Spawner>();
        MainManager.instance.SetCurrentLevelKills(_levelEnemiesAmount);
        DistributeZombies();
    }

    #endregion

    #region METHODS

    private void BoostEnemies()
    {
        if (MainManager.instance.CurrentLevel() == 1) return;
        if (MainManager.instance.CurrentLevel() % 2 == 1) {
            // Boost stats
            _levelEnemiesStatsBoostPercent *= 1.05f; // 5 % more life and damage
        } else {
            // Boost amount
            _levelEnemiesAmount = (int) Math.Ceiling(_levelEnemiesAmount * 1.2f); // 20% more enemies
        }
        MainManager.instance.SetCurrentLevelKills(_levelEnemiesAmount);
    }

    private void DistributeZombies()
    {
        int[] enemyOnSpawners = new int[_spawners.Length];
        for (int i = 0; i < _levelEnemiesAmount; i++) {
            enemyOnSpawners[i%enemyOnSpawners.Length]++;
        }
        for (int i = 0; i < _spawners.Length; i++) {
            _spawners[i].SetLevelStats(enemyOnSpawners[i], _levelEnemiesStatsBoostPercent);
            StartCoroutine(_spawners[i].Spawn());
        }
    }

    #endregion
}
