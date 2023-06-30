using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{

    private Light _light;

    void Start()
    {
        _light = GetComponentInChildren<Light>();
        StartCoroutine(FlashLightCoroutine());
    }

    IEnumerator FlashLightCoroutine() 
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3f, 10f));
            for (int i = 0; i < 4; i++)
            {
                _light.enabled = !_light.enabled;
                yield return new WaitForSeconds(Random.Range(0.1f, 0.2f));
            }
        }
    }
}
