using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalRotation : MonoBehaviour
{
    public float RotationSpeed => _rotationSpeed;
    [SerializeField] private float _rotationSpeed = 2f;

    public float mouseSnappiness = 20f;              // default was 10f; larger values of this cause less filtering, more responsiveness
    private float accMouseY = 0;                     // reference for mouse look smoothing
    public float clampLookY = 90f;                   // maximum look up/down angle
    public float xRotation = 0f;                     // the up/down angle the player is looking



    // Update is called once per frame
    void Update()
    {
        float inputLookY = Input.GetAxis( "Mouse Y" );

        accMouseY = Mathf.Lerp( accMouseY, inputLookY, mouseSnappiness * Time.deltaTime );
 
        float mouseY = accMouseY * _rotationSpeed * 100f * Time.deltaTime;
 
        // rotate camera X
        xRotation += -mouseY ;
        xRotation = Mathf.Clamp( xRotation, -clampLookY, clampLookY );
 
        transform.localRotation = Quaternion.Euler( xRotation, 0f, 0f );
    }
}
