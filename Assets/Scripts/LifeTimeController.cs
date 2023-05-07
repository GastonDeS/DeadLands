using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTimeController : MonoBehaviour
{
    private float _lifeTime = 5f;
    private float _currentLifeTime;

    public bool IsLifeTimeOver() => _currentLifeTime <= 0;

    void Start()
    {
        _currentLifeTime = _lifeTime;
    }

    void Update()
    {
        _currentLifeTime -= Time.deltaTime;
    }


    public void SetLifeTime(float value)
    {
        _lifeTime = value;
        _currentLifeTime = _lifeTime;
    }

    private void OnDestroy()
    {
        Debug.Log("Bullet destroyed");
    }
}
