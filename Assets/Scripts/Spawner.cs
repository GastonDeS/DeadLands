using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    GameObject Prefab => _prefab;
    [SerializeField] private GameObject _prefab;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(5.0f);
        GameObject newEnemy = Instantiate(_prefab, transform.position, transform.rotation);
        StartCoroutine(Spawn());
    }
}
