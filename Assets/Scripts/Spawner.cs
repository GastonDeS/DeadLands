using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    GameObject Prefab => _prefab;
    [SerializeField] private GameObject _prefab;

    GameObject MainCharacter => _mainCharacter;
    [SerializeField] private GameObject _mainCharacter;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(5.0f);
        GameObject newEnemy = Instantiate(_prefab, transform.position, transform.rotation);
        newEnemy.GetComponent<Enemy>().SetAgent2(_mainCharacter);
        StartCoroutine(Spawn());
    }
}
