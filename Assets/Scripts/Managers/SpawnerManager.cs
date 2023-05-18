using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    private int _level = 1;
    private float _levelEnemiesAmount = 5;
    private float _levelEnemiesStatsBoostPercent = 1;
    private Spawner[] _spawners;

    // Start is called before the first frame update
    void Start()
    {
        _spawners = GetComponentsInChildren<Spawner>();
        StartCoroutine(StartSpawning());
    }

    IEnumerator StartSpawning()
    {
        while (true) {
            UpdateEnemyStats();
            DistributeZombies();
            yield return new WaitForSeconds(20f);
            _level++;
        }
    }

    void UpdateEnemyStats()
    {
        if (_level == 1) return;
        if (_level % 2 == 1) {
            // upgrade life
            _levelEnemiesStatsBoostPercent *= 1.05f; // 5 % more life
        } else {
            // update amount
            _levelEnemiesAmount *= 1.2f; // 20 % more enemies
        }
    }

    void DistributeZombies()
    {
        int[] enemyOnSpawners = new int[_spawners.Length];
        for (int i = 0; i < _levelEnemiesAmount; i++) {
            int randomSpawnerIndex = Random.Range(0, _spawners.Length);
            enemyOnSpawners[randomSpawnerIndex]++;
        }
        for (int i = 0; i < _spawners.Length; i++) {
            _spawners[i].SetLevelStats(enemyOnSpawners[i], _levelEnemiesStatsBoostPercent);
            StartCoroutine(_spawners[i].Spawn());
        }
    }
}
