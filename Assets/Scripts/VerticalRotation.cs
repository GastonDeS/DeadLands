using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalRotation : MonoBehaviour
{
    public float RotationSpeed => _rotationSpeed;
    [SerializeField] private float _rotationSpeed = 600f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y"), 0, 0) * Time.deltaTime * RotationSpeed);
    }
}
