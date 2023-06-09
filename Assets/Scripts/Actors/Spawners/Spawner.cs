using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    GameObject Prefab => _prefab;
    [SerializeField] private GameObject _prefab;

    private float _spawnRate = 1f;
    private float _statsBoost = 1f;

    private int _enemyCount = 0;

    public void SetLevelStats(int enemyCount, float statsBoost)
    {
        _enemyCount = enemyCount;
        _statsBoost = statsBoost;
    }

    public IEnumerator Spawn()
    {
        if (_enemyCount <= 0) yield break;
        GameObject newEnemy = Instantiate(_prefab, transform.position, transform.rotation);
        newEnemy.GetComponent<Enemy>().BoostStats(_statsBoost);
        _enemyCount--;
        yield return new WaitForSeconds(_spawnRate);
        StartCoroutine(Spawn());
    }
}
