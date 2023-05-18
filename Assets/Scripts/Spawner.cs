using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    GameObject Prefab => _prefab;
    [SerializeField] private GameObject _prefab;

    GameObject MainCharacter => _mainCharacter;
    [SerializeField] private GameObject _mainCharacter;

    private float _spawnRate = 1f;
    private float _statsBoost = 1f;

    private int _enemyCount = 0;

    void Start()
    {
        
    }

    public void SetLevelStats(int enemyCount, float statsBoost )
    {
        _enemyCount = enemyCount;
        _statsBoost = statsBoost;
    }

    public IEnumerator Spawn()
    {
        if (_enemyCount <= 0) yield break;
        yield return new WaitForSeconds(_spawnRate);
        GameObject newEnemy = Instantiate(_prefab, transform.position, transform.rotation);
        newEnemy.GetComponent<Enemy>().SetAgent2(_mainCharacter);
        newEnemy.GetComponent<Enemy>().BoostStats(_statsBoost);
        _enemyCount--;
        StartCoroutine(Spawn());
    }
}
